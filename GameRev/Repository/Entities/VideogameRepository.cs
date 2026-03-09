using Microsoft.EntityFrameworkCore;
using GameRev.Data;
using GameRev.Models;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Repository.Generic;

namespace GameRev.Repository.Entities;

public class VideogameRepository : GenericCrudRepository<Videogame>, IVideogameRepository
{
    private readonly IPlatformRepository platformRepository;
    public VideogameRepository(AppDbContext context, IPlatformRepository platformRepository) : base(context)
    {
        this.platformRepository = platformRepository;
    }

    public async Task<List<Videogame>> GetByPlatform(string platform, CancellationToken ct)
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

    public async Task<Videogame?> GetByTitle(string title, CancellationToken ct)
    {
        return await context.Videogames.FirstOrDefaultAsync(v => v.Title.Equals(title),ct);
    }
}