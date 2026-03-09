using Microsoft.EntityFrameworkCore;
using GameRev.Data;
using GameRev.Models;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Repository.Generic;

namespace GameRev.Repository.Entities;

public class PlatformRepository : GenericCrudRepository<Platform>, IPlatformRepository
{

    public PlatformRepository(AppDbContext context) : base(context) {}
    public async Task<Platform?> GetByNameAsync(string name, CancellationToken ct)
    {
        return await context.Platforms.FirstOrDefaultAsync(p => p.Name.Equals(name),ct);
    }
}