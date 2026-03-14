using GameRev.DTOs.Requests;
using GameRev.DTOs.Requests.Update;
using GameRev.DTOs.Responses;
using GameRev.Models.Entities;

namespace GameRev.Services.Entities.Interfaces;

public interface IReviewService
{
    Task<ReviewResponse?> AddAsync (ReviewRequest request, CancellationToken ct);

    Task<ReviewResponse?> GetByIdAsync (long id, CancellationToken ct);

    Task<List<ReviewResponse>> GetAllAsync (CancellationToken ct);

    Task<bool> DeleteAsync (long id, CancellationToken ct);

    Task<bool> UpdateAsync (UpdateReviewRequest request, CancellationToken ct);

    Task<List<ReviewResponse>> GetGameReviewsAsync (long gameId, CancellationToken ct);

    Task<List<ReviewResponse>> GetUserReviewsAsync (long userId, CancellationToken ct);
}