using GameRev.DTOs.Requests;
using GameRev.DTOs.Requests.Update;
using GameRev.DTOs.Responses;

namespace GameRev.Services.Entities.Interfaces;

public interface IUserService
{
    Task<UserResponse?> AddAsync (UserRequest request, CancellationToken ct);

    Task<UserResponse?> GetByIdAsync (long id, CancellationToken ct);
    
    Task<List<UserResponse>> GetAllAsync (CancellationToken ct);

    Task<bool> RemoveAsync (long id, CancellationToken ct);

    Task<bool> UpdateAsync (UpdateUserRequest request, CancellationToken ct);

    Task<UserResponse?> GetByEmailAsync (string email, CancellationToken ct);
    
    Task<UserResponse?> GetByUsernameAsync (string username, CancellationToken ct);

}