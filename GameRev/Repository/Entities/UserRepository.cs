using GameRev.Data;
using GameRev.Models;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace GameRev.Repository.Entities;

public class UserRepository : GenericCrudRepository<User>, IUserRepository
{

    public UserRepository(AppDbContext context) : base (context){}

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email), ct);
    }

    public async Task<User?> GetByUsername(string username, CancellationToken ct)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Username.Equals(username), ct);
    }
}