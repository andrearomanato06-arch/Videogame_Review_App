using GameRev.Models;
using GameRev.Repository.Generic;

namespace GameRev.Repository.Entities.Interfaces;

public interface IVideogameRepository : IGenericCrudRepository<Videogame>
{
    Task<Videogame?> GetByTitle (string title, CancellationToken ct);

    Task<List<Videogame>> GetByPlatform (string platform, CancellationToken ct);
}