namespace GameRev.Services.Entities.Interfaces;

using GameRev.DTOs.Requests;
using GameRev.DTOs.Requests.Update;
using GameRev.DTOs.Responses;

public interface IAuthorService{

    Task<AuthorResponse?> AddAsync (AuthorRequest request, CancellationToken ct);

    Task<bool> RemoveAsync (long id, CancellationToken ct);

    Task<bool> UpdateAsync (UpdateAuthorRequest request, CancellationToken ct);

    Task<AuthorResponse?> GetByIdAsync (long id, CancellationToken ct);

    Task<List<AuthorResponse>> GetAllAsync (CancellationToken ct);
}