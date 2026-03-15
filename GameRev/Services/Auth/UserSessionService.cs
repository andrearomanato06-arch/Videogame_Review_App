using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameRev.DTOs.Responses;
using GameRev.Models.Auth;
using GameRev.Models.Entities;
using GameRev.Repository.Auth.Interfaces;
using GameRev.Repository.Entities.Interfaces;
using GameRev.Services.Auth.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace GameRev.Services.Auth;

public class UserSessionService : IUserSessionService
{

    private readonly IUserSessionRepository userSessionRepository;
    private readonly IUserRepository userRepository;

    public UserSessionService (IUserSessionRepository userSessionRepository, IUserRepository userRepository)
    {
        this.userSessionRepository = userSessionRepository;
        this.userRepository = userRepository;
    }

    public async Task<UserSession?> AddAsync(UserSession userSession, CancellationToken ct)
    {
        return await userSessionRepository.AddAsync(userSession,ct);
    }

    public async Task<bool> EndSessionAsync(UserSession session, CancellationToken ct)
    {
        return await userSessionRepository.EndSessionAsync(session,ct);
    }

    public async Task<UserResponse?> GetUserBySessionIdAsync(long id, CancellationToken ct)
    {
        return await userSessionRepository.GetUserBySessionIdAsync(id,ct);
    }

    public async Task<UserSession?> GetByJtid(long jtid, CancellationToken ct)
    {
        return await userSessionRepository.GetByJtid(jtid,ct);
    }

    public async Task<bool> UpdateJtidSession(UserSession newSession, CancellationToken ct)
    {
        return await userSessionRepository.UpdateJtidSession(newSession,ct);
    }

    public async Task<string?> GenerateJwtToken(User user, CancellationToken ct)
    {
        var searchedUser = await CheckIfUserExist(user,ct);
        if(searchedUser is null)
        {
            return null;
        }

        var session = await CheckIfUserSessionAlredyExist(searchedUser,ct);
        session = await userSessionRepository.AddAsync(session,ct);
        if(session is null)
        {
            return null;
            //log error
        }

        //! ENV DATA
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SetUpIn.ENV_FILE_PLS"));
        var duration = session.IssuedAt.AddMinutes(60); // configure from .env
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new []{ 
            new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim (ClaimTypes.Email, user.Email),
            new Claim (JwtRegisteredClaimNames.Jti, session.Jtid.ToString()),
            new Claim (JwtRegisteredClaimNames.Iat, duration.ToString(), ClaimValueTypes.Integer64)
        };

        var token = new JwtSecurityToken(
            issuer : "configure in .env", //! ENV DATA
            audience: null,
            claims: claims,
            signingCredentials: signingCredentials,
            expires : duration
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<User?> CheckIfUserExist(User user, CancellationToken ct)
    {
        var searchedUser = await userRepository.GetByIdAsync(user.Id, ct);
        if(searchedUser is null)
        {
            return null;
            //log
        }
        return searchedUser;
    }

    private async Task<UserSession> CheckIfUserSessionAlredyExist(User user, CancellationToken ct)
    {
        var session = await userSessionRepository.GetSessionByUserId(user.Id, ct);
        if(session is not null)
        {
           await userSessionRepository.EndSessionAsync(session,ct);
           //log old session destroyed
        }
        var issueTime = DateTime.Now;
        return new UserSession
        {
            UserId = user.Id,
            IssuedAt = issueTime
        };
    }
}