using GameRev.Data;
using GameRev.DTOs.Mappers;
using GameRev.DTOs.Responses;
using GameRev.Models.Auth;
using GameRev.Repository.Auth.Interfaces;
using GameRev.Repository.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameRev.Repository.Auth;

public class JwtTokenRepository : IUserSessionRepository
{
    private readonly AppDbContext context;
    private readonly IUserRepository userRepo;
    public JwtTokenRepository(AppDbContext context, IUserRepository userRepo)
    {
        this.context = context;
        this.userRepo = userRepo;
    }

    public async Task<UserSession?> AddAsync(UserSession userSession, CancellationToken ct)
    {
        await context.UserSessions.AddAsync(userSession,ct);
        return await context.SaveChangesAsync(ct) > 0 
        ? userSession 
        : null;
    }

    public async Task<bool> EndSessionAsync(UserSession session, CancellationToken ct)
    {
        context.UserSessions.Remove(session);
        return await context.SaveChangesAsync(ct) > 0;
    }

    public async Task<UserResponse?> GetUserBySessionIdAsync(long id, CancellationToken ct)
    {
        var session = await context.UserSessions.Where(us => us.Jtid == id).FirstOrDefaultAsync(ct);
        if(session is null)
        {
            return null;
            //log
        }
        if(session.UserId is null)
        {
            return null;
            //log
        }
        var user = await userRepo.GetByIdAsync((long) session.UserId,ct);
        return user is not null 
        ? ModelsToDtos.UserToUserResponse(user)
        : null;
    }

    public async Task<bool> UpdateJtidSession(UserSession newSession, CancellationToken ct)
    {
        var session = await context.UserSessions.Where(us => us.UserId == newSession.UserId).FirstOrDefaultAsync(ct);
        if(session is null)
        {
            return false;
            //log
        }
        context.UserSessions.Remove(session);
        await context.UserSessions.AddAsync(newSession,ct);
        return await context.SaveChangesAsync(ct) > 0;
    }

    public async Task<UserSession?> GetByJtid(long jtid, CancellationToken ct)
    {
        var session = await context.UserSessions.Where(us => us.Jtid == jtid).FirstOrDefaultAsync(ct);
        return session is not null
        ? session
        : null;
    }

    public async Task<UserSession?> GetSessionByUserId (long userId, CancellationToken ct)
    {
        return await context.UserSessions.Where(us => us.UserId == userId).FirstOrDefaultAsync(ct);
    }
}