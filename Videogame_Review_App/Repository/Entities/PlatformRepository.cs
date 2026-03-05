using Microsoft.EntityFrameworkCore;
using Videogame_Review_App.Data;
using Videogame_Review_App.Models;
using Videogame_Review_App.Repository.Entities.Interfaces;
using Videogame_Review_App.Repository.Generic;

namespace Videogame_Review_App.Repository.Entities;

public class PlatformRepository : GenericCrudRepository<Platform>, IPlatformRepository
{

    public PlatformRepository(AppDbContext context) : base(context) {}
    public async Task<Platform?> GetByNameAsync(string name, CancellationToken ct)
    {
        return await context.Platforms.FirstOrDefaultAsync(p => p.Name.Equals(name),ct);
    }
}