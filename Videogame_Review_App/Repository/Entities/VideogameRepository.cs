using Microsoft.EntityFrameworkCore;
using Videogame_Review_App.Data;
using Videogame_Review_App.Models;
using Videogame_Review_App.Repository.Entities.Interfaces;
using Videogame_Review_App.Repository.Generic;

namespace Videogame_Review_App.Repository.Entities;

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