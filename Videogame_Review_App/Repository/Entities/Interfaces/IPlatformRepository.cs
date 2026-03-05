using Videogame_Review_App.Models;
using Videogame_Review_App.Repository.Generic;

namespace Videogame_Review_App.Repository.Entities.Interfaces;

public interface IPlatformRepository : IGenericCrudRepository<Platform>
{
    Task<Platform?> GetByNameAsync (string name, CancellationToken ct);
}