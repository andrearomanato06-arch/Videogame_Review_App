using Microsoft.EntityFrameworkCore;
using GameRev.Data;
using GameRev.Models.Entities;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Repository.Generic;
using GameRev.DTOs.Filters;

namespace GameRev.Repository.Entities;

public class VideogameRepository : GenericCrudRepository<Videogame>, IVideogameRepository
{
    private readonly IPlatformRepository platformRepository;
    public VideogameRepository(AppDbContext context, IPlatformRepository platformRepository) : base(context)
    {
        this.platformRepository = platformRepository;
    }

    public async Task<List<Videogame>> GetByPlatformAsync (string platform, CancellationToken ct)
    {
        var selectedPlatform = await platformRepository.GetByNameAsync(platform,ct);
        if(selectedPlatform is null)
        {
            return [];
        }
        return await context.Videogames
        .Where(v => v.Platforms.Any(p => p.Name.ToLower().Equals(platform.ToLower())))
        .Include(v => v.Platforms)
        .AsNoTracking()
        .ToListAsync(ct);
    }

    public async Task<Videogame?> GetByTitleAsync (string title, CancellationToken ct)
    {
        return await context.Videogames.FirstOrDefaultAsync(v => v.Title.Equals(title),ct);
    }

    public async Task<List<Videogame>> GetNewAsync (CancellationToken ct)
    {
        DateOnly timeSpan = DateOnly.FromDateTime(DateTime.Now).AddDays(-60);
        return await context.Videogames.Where(v => v.ReleaseDate >=  timeSpan ).ToListAsync(ct);
    }

    public async Task<List<Videogame>> GetMostLikedAsync (int elementsToShow , CancellationToken ct)
    {
        elementsToShow = elementsToShow < 10 ? 10 : elementsToShow;
        return await context.Videogames
            .Where(v => v.Reviews.Any())
            .Select( v => new
                {
                    Game = v,
                    AvgRating = v.Reviews.Average(r => r.Rating)
                }
            )
            .Where(result => result.AvgRating >= 4.0 && result.AvgRating <= 5.0)
            .OrderByDescending(x => x.AvgRating)
            .Select(result => result.Game)
            .Take(elementsToShow)
            .ToListAsync(ct);
    }

    public async Task<List<Videogame>> SearchAsync (VideogameSearchFilter filter, CancellationToken ct)
    {
        var query = CreateQuery(filter);
        return await query.OrderBy(v => v.Title).ToListAsync(ct);
    }

    private IQueryable<Videogame> CreateQuery (VideogameSearchFilter filter)
    {
        IQueryable<Videogame> query = context.Videogames.AsNoTracking();

        if(filter.Title != null)
            query = query.Where(v => v.Title.ToLower().Contains(filter.Title.ToLower()));

        if(filter.Platform != null)
            query = query.Where(v => v.Platforms.Any(p => p.Name.ToLower().Contains(filter.Platform.ToLower())));

        if(filter.Year != null)
        {
            var firstOftheYear = new DateOnly((int)filter.Year, 1,1);
            var lastOfTheYear = new DateOnly((int)filter.Year,12,31);
            query = query.Where(v => v.ReleaseDate >= firstOftheYear && v.ReleaseDate <= lastOfTheYear);
        }

        if(filter.Objectives != null)
            query = query.Where(v => v.Objectives >= filter.Objectives);
        

        query = FilterByRating(query, filter.RatingStart, filter.RatingEnd);
        
        return query;
    }

    private IQueryable<Videogame> FilterByRating(IQueryable<Videogame> query, double? start, double? end)
    {
        start = start is null ? 1.0 : start;
        end = end is null ? 5.0 : end;
        
        return query
            .Where(v => v.Reviews.Any())
            .Select(v => new
            {
                Videogame = v,
                AvgRating = v.Reviews.Average(r => r.Rating)
            })
            .Where(x => x.AvgRating >= start && x.AvgRating <= end)
            .Select(x => x.Videogame);
    }
}