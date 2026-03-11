using GameRev.Models.Auth;
using GameRev.Models.Entities;

namespace GameRev.Repository.Auth.Interfaces;

public interface IJwtTokenRepository
{
    Task<JwtToken?> AddAsync (JwtToken token, CancellationToken ct);

    Task<bool> RevokeTokenAsync (JwtToken token, CancellationToken ct);

    Task<JwtToken?> GetByTokenAsync (string token, CancellationToken ct);

    Task<JwtToken?> GetByTokenAsync (long id, CancellationToken ct);

    Task<User?> GetUserByTokenAsync (string token, CancellationToken ct);
}