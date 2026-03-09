using GameRev.Models;
using GameRev.Repository.Generic;

namespace GameRev.Repository.Entities.Interfaces;

public interface IUserRepository : IGenericCrudRepository<User>
{
    Task<User?> GetByEmailAsync (string email, CancellationToken ct);

    Task<User?> GetByUsername (string username, CancellationToken ct);
}