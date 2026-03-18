using GameRev.DTOs.Filters;
using GameRev.DTOs.Requests;
using GameRev.DTOs.Requests.Update;
using GameRev.DTOs.Responses;

namespace GameRev.Services.Entities.Interfaces;

public interface IVideogameService
{
    Task<VideogameResponse?> AddAsync (VideogameRequest request, CancellationToken ct);

    Task<VideogameResponse?> GetByIdAsync (long id, CancellationToken ct);

    Task<List<VideogameResponse>> GetAllAsync (CancellationToken ct);

    Task<bool> RemoveAsync (long id, CancellationToken ct);

    Task<bool> UpdateAsync (UpdateVideogameRequest request, CancellationToken ct);

    Task<VideogameResponse?> GetByTitleAsync (string title, CancellationToken ct);

    Task<List<VideogameResponse>> GetByPlatformAsync (string platform, CancellationToken ct);

    Task<PagedResponse<MinimalVideogameResponse>> GetNewAsync (int page, int elementsToShow, CancellationToken ct);

    Task<PagedResponse<MinimalVideogameResponse>> GetMostLikedAsync (int page, int elementsToShow, CancellationToken ct);

    Task<PagedResponse<MinimalVideogameResponse>> SearchAsync (VideogameSearchFilter filter, int page, int elementsToShow, CancellationToken ct);

    Task<List<MinimalVideogameResponse>> GetCasualGames (int limit, CancellationToken ct);
}