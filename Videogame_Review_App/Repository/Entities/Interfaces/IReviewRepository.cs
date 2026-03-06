using Videogame_Review_App.Models;
using Videogame_Review_App.Repository.Generic;

namespace Videogame_Review_App.Repository.Entities.Interfaces;

public interface IReviewRepository : IGenericCrudRepository<Review>
{
    Task<List<Review>> GetGameReviews (long gameId, CancellationToken ct);

    Task<List<Review>> GetUserReviews (long userId, CancellationToken ct);

}