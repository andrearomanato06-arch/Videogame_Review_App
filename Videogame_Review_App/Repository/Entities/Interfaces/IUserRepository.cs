using Videogame_Review_App.Models;
using Videogame_Review_App.Repository.Generic;

namespace Videogame_Review_App.Repository.Entities.Interfaces;

public interface IUserRepository : IGenericCrudRepository<User>
{
    Task<User?> GetByEmailAsync (string email, CancellationToken ct);

    Task<User?> GetByUsername (string username, CancellationToken ct);
}