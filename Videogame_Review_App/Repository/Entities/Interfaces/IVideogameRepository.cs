using Videogame_Review_App.Models;
using Videogame_Review_App.Repository.Generic;

namespace Videogame_Review_App.Repository.Entities.Interfaces;

public interface IVideogameRepository : IGenericCrudRepository<Videogame>
{
    Task<Videogame?> GetByTitle (string title, CancellationToken ct);

    Task<List<Videogame>> GetByPlatform (string platform, CancellationToken ct);
}