using Microsoft.EntityFrameworkCore;
using GameRev.Data;
using GameRev.Models;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Repository.Generic;

namespace GameRev.Repository.Entities;

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