using GameRev.Models;
using GameRev.Repository.Generic;

namespace GameRev.Repository.Entities.Interfaces;

public interface IReviewRepository : IGenericCrudRepository<Review>
{
    Task<List<Review>> GetGameReviews (long gameId, CancellationToken ct);

    Task<List<Review>> GetUserReviews (long userId, CancellationToken ct);

}