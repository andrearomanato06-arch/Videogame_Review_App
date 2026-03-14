using GameRev.DTOs.Mappers;
using GameRev.DTOs.Requests;
using GameRev.DTOs.Requests.Update;
using GameRev.DTOs.Responses;
using GameRev.Models.Entities;
using GameRev.Models.Utils;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Services.Entities.Interfaces;

namespace GameRev.Services.Entities;

public class ReviewService : IReviewService
{
    
    private readonly IReviewRepository reviewRepository;
    private readonly IVideogameRepository videogameRepository;
    private readonly IUserRepository userRepository;

    public ReviewService(IReviewRepository reviewRepository, IVideogameRepository videogameRepository, IUserRepository userRepository)
    {
        this.reviewRepository = reviewRepository;
        this.videogameRepository = videogameRepository;
        this.userRepository = userRepository;
    }

    public async Task<ReviewResponse?> AddAsync(ReviewRequest request, CancellationToken ct)
    {
        var review = DtosToModels.ReviewRequestToReview(request);
        var response = await reviewRepository.AddAsync(review,ct);
        return response is not null
        ? ModelsToDtos.ReviewToReviewResponse(response)
        : null;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct)
    {
        var review = await reviewRepository.GetByIdAsync(id,ct);
        if(review is null) return false;
        return await reviewRepository.DeleteAsync(review,ct);
    }

    public async Task<List<ReviewResponse>> GetAllAsync(CancellationToken ct)
    {
        var responses = await reviewRepository.GetAllAsync(ct);
        return ModelsToDtos.ReviewToReviewResponse(responses);
    }

    public async Task<ReviewResponse?> GetByIdAsync(long id, CancellationToken ct)
    {
        var review = await reviewRepository.GetByIdAsync(id,ct);
        return review is not null
        ? ModelsToDtos.ReviewToReviewResponse(review)
        : null;
    }

    public async Task<List<ReviewResponse>> GetGameReviewsAsync(long gameId, CancellationToken ct)
    {
        var videogame = await videogameRepository.GetByIdAsync(gameId, ct);
        if(videogame is null) return [];
        var reviews = await reviewRepository.GetGameReviewsAsync(gameId,ct);
        return ModelsToDtos.ReviewToReviewResponse(reviews);
    }

    public async Task<List<ReviewResponse>> GetUserReviewsAsync(long userId, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(userId,ct);
        if(user is null) return [];
        var reviews = await reviewRepository.GetUserReviewsAsync(userId,ct);
        return ModelsToDtos.ReviewToReviewResponse(reviews);
    }

    public async Task<bool> UpdateAsync(UpdateReviewRequest request, CancellationToken ct)
    {
        var review = await reviewRepository.GetByIdAsync(request.Id,ct);
        if(review is null) return false;
        UpdateModel.UpdateReviewFromDto(review,request);
        return await reviewRepository.UpdateAsync(review,ct);
    }
}