using GameRev.DTOs.Responses;
using GameRev.Models.Auth;
using GameRev.Models.Entities;

namespace GameRev.Services.Auth.Interfaces;

public interface IUserSessionService
{
    Task<UserSession?> AddAsync (UserSession userSession, CancellationToken ct);

    Task<bool> EndSessionAsync (UserSession session, CancellationToken ct);

    Task<UserResponse?> GetUserBySessionIdAsync (long id, CancellationToken ct);

    Task<bool> UpdateJtidSession (UserSession newSession, CancellationToken ct);

    Task<UserSession?> GetByJtid(long jtid, CancellationToken ct);

    Task<string?> GenerateJwtToken (User user, CancellationToken ct);
}