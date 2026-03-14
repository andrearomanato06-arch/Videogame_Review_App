namespace GameRev.Services.Interfaces;

using GameRev.DTOs.Requests;
using GameRev.DTOs.Requests.Update;
using GameRev.DTOs.Responses;
public interface IPlatformService
{
    Task<PlatformResponse?> GetByIdAsync (long id, CancellationToken ct);

    Task<List<PlatformResponse>> GetAllAsync (CancellationToken ct);

    Task<PlatformResponse?> AddAsync (PlatformRequest request, CancellationToken ct);

    Task<bool> UpdatePlatformAsync (UpdatePlatformRequest request, CancellationToken ct);

    Task<bool> DeleteAsync (long id, CancellationToken ct);

    Task<PlatformResponse?> GetByNameAsync (string name, CancellationToken ct);
}