using GameRev.Data;
using GameRev.Models.Auth;
using GameRev.Models.Entities;
using GameRev.Repository.Auth.Interfaces;
using GameRev.Repository.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameRev.Repository.Auth;

public class JwtTokenRepository : IJwtTokenRepository
{
    private readonly AppDbContext context;
    private readonly IUserRepository userRepo;
    public JwtTokenRepository(AppDbContext context, IUserRepository userRepo)
    {
        this.context = context;
        this.userRepo = userRepo;
    }

    public async Task<JwtToken?> AddAsync(JwtToken token, CancellationToken ct)
    {
        await context.JwtTokens.AddAsync(token,ct);
        bool success = await context.SaveChangesAsync(ct) > 0;
        return success ? token : null;

    }

    public async Task<JwtToken?> GetByTokenAsync(string token, CancellationToken ct)
    {
        return await context.JwtTokens.Where(j => j.Token.Equals(token)).FirstOrDefaultAsync(ct);
    }

    public async Task<JwtToken?> GetByTokenAsync(long id, CancellationToken ct)
    {
        return await context.JwtTokens.FindAsync([id, ct], cancellationToken: ct);
    }

    public async Task<User?> GetUserByTokenAsync(string token, CancellationToken ct)
    {
        var jwt = await GetByTokenAsync(token,ct);
        return jwt is null || jwt.UserId is null
            ?  null 
            : await userRepo.GetByIdAsync((long) jwt.UserId, ct);
    }

    public async Task<bool> RevokeTokenAsync(JwtToken token, CancellationToken ct)
    {
        context.JwtTokens.Remove(token);
        return await context.SaveChangesAsync(ct) > 0;
    }
}