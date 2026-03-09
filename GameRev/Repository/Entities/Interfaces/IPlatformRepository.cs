using GameRev.Models;
using GameRev.Repository.Generic;

namespace GameRev.Repository.Entities.Interfaces;

public interface IPlatformRepository : IGenericCrudRepository<Platform>
{
    Task<Platform?> GetByNameAsync (string name, CancellationToken ct);
}