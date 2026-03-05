using Videogame_Review_App.Data;
using Videogame_Review_App.Models;
using Videogame_Review_App.Repository.Entities.Interfaces;
using Videogame_Review_App.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace Videogame_Review_App.Repository.Entities;

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