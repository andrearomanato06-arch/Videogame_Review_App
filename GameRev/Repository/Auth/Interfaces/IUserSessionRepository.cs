using GameRev.DTOs.Responses;
using GameRev.Models.Auth;

namespace GameRev.Repository.Auth.Interfaces;

public interface IUserSessionRepository
{
    Task<UserSession?> AddAsync (UserSession userSession, CancellationToken ct);

    Task<bool> EndSessionAsync (UserSession session, CancellationToken ct);

    Task<UserResponse?> GetUserBySessionIdAsync (long id, CancellationToken ct);

    Task<bool> UpdateJtidSession (UserSession newSession, CancellationToken ct);

    Task<UserSession?> GetByJtid(long jtid, CancellationToken ct);

    Task<UserSession?> GetSessionByUserId (long userId, CancellationToken ct);
}