using Microsoft.EntityFrameworkCore;
using Videogame_Review_App.Data;
using Videogame_Review_App.Models;
using Videogame_Review_App.Repository.Entities.Interfaces;
using Videogame_Review_App.Repository.Generic;

namespace Videogame_Review_App.Repository.Entities;

public class ReviewRepository : GenericCrudRepository<Review>, IReviewRepository
{
    public ReviewRepository(AppDbContext context) : base(context){}

    public async Task<List<Review>> GetGameReviews(long gameId, CancellationToken ct)
    {
        return await context.Reviews.Where(r => r.VideogameId == gameId).AsNoTracking().ToListAsync(ct);
    }

    public async Task<List<Review>> GetUserReviews(long userId, CancellationToken ct)
    {
        return await context.Reviews.Where(r => r.UserId == userId).AsNoTracking().ToListAsync(ct);
    }
}