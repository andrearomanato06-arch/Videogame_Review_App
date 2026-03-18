using Microsoft.EntityFrameworkCore;
using GameRev.Data;
using GameRev.Models.Entities;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Repository.Generic;
using GameRev.DTOs.Filters;
using GameRev.DTOs.Responses;
using GameRev.DTOs.Mappers;
using System.Security.Principal;

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
        .Where(v => v.Platforms.Any(p => p.Name.Equals(platform)))
        .Include(v => v.Platforms)
        .AsNoTracking()
        .ToListAsync(ct);
    }

    public async Task<Videogame?> GetByTitleAsync (string title, CancellationToken ct)
    {
        return await context.Videogames.FirstOrDefaultAsync(v => v.Title.Contains(title),ct);
    }

    public async Task<PagedResponse<MinimalVideogameResponse>> GetNewAsync (int page, int elementsToShow, CancellationToken ct)
    {
        DateOnly timeSpan = DateOnly.FromDateTime(DateTime.Now).AddDays(-60);

        var query = context.Videogames.Where(v => v.ReleaseDate >=  timeSpan );
        var totalElements = await query.CountAsync(ct);

        var videogames = await query
        .OrderByDescending(g => g.ReleaseDate)
        .Skip((page - 1) * elementsToShow)
        .Take(elementsToShow)
        .ToListAsync(ct);

        return new PagedResponse<MinimalVideogameResponse>
        {
           Elements = ModelsToDtos.VideogameToMinimalVideogameResponse(videogames),
           CurrentPage = page,
           TotalElements =  totalElements,
           TotalPages = (int)Math.Ceiling(totalElements/(double)elementsToShow)
        }; 
    }

    public async Task<PagedResponse<MinimalVideogameResponse>> GetMostLikedAsync (int page, int elementsToShow , CancellationToken ct)
    {
        elementsToShow = elementsToShow < 10 ? 10 : elementsToShow;

        var query = context.Videogames
            .Where(v => v.Reviews.Any())
            .Select( v => new
                {
                    Game = v,
                    AvgRating = v.Reviews.Average(r => r.Rating)
                }
            )
            .Where(result => result.AvgRating >= 3.5 && result.AvgRating <= 5.0);
        
        var totalElements = await query.CountAsync(ct);

        var videogames = await query
            .OrderByDescending(g => g.AvgRating)
            .Skip((page - 1) * elementsToShow)
            .Take(elementsToShow)
            .Select(g => g.Game)
            .ToListAsync(ct);
        
        return new PagedResponse<MinimalVideogameResponse>
        {
           Elements = ModelsToDtos.VideogameToMinimalVideogameResponse(videogames),
           CurrentPage = page,
           TotalElements = totalElements,
           TotalPages = (int) Math.Ceiling(totalElements/(double)elementsToShow) 
        }; 
    }

    public async Task<PagedResponse<MinimalVideogameResponse>> SearchAsync (VideogameSearchFilter filter, int page, int elementsToShow, CancellationToken ct)
    {
        var query = CreateQuery(filter).OrderBy(v => v.Title);
        var totalElements = await query.CountAsync(ct);

        var videogames = await query.
            Skip((page-1)*elementsToShow)
            .Take(elementsToShow)
            .ToListAsync(ct);

        return new PagedResponse<MinimalVideogameResponse>
        {
            Elements = ModelsToDtos.VideogameToMinimalVideogameResponse(videogames),
            CurrentPage = page,
            TotalElements = totalElements,
            TotalPages = (int)Math.Ceiling(totalElements/(double)elementsToShow)
        };
    }

    private IQueryable<Videogame> CreateQuery (VideogameSearchFilter filter)
    {
        IQueryable<Videogame> query = context.Videogames.AsNoTracking();

        if(filter.Title != null)
            query = query.Where(v => v.Title.ToLower().Contains(filter.Title.ToLower()));

        if(filter.Platform != null)
            query = query.Where(v => v.Platforms.Any(p => p.Name.Contains(filter.Platform)));

        if(filter.Year != null)
        {
            var firstOftheYear = new DateOnly((int)filter.Year, 1,1);
            var lastOfTheYear = new DateOnly((int)filter.Year,12,31);
            query = query.Where(v => v.ReleaseDate >= firstOftheYear && v.ReleaseDate <= lastOfTheYear);
        }

        if(filter.Objectives != null)
            query = query.Where(v => v.Objectives >= filter.Objectives);
        
        if(filter.RatingStart != null || filter.RatingEnd != null)
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

    public async Task<List<MinimalVideogameResponse>> GetCasualGames (int limit, CancellationToken ct)
    {
        var videogames = await context.Videogames
            .OrderBy(g => Guid.NewGuid())
            .Take(limit)
            .ToListAsync(ct);

        return ModelsToDtos.VideogameToMinimalVideogameResponse(videogames);
    }

    public async new Task<VideogameResponse> GetByIdAsync(long id, CancellationToken ct)
    {
        var videogame = await context.Videogames
        .Where(v => v.Id == id)
        .Select(gr => new
        {
            Videogame = gr,
            AvgRating = gr.Reviews.Average(r => r.Rating)
        })
        .FirstOrDefaultAsync(ct);

        return ModelsToDtos.VideogameToVideogameResponse(videogame.Videogame, videogame.AvgRating);
    }
}