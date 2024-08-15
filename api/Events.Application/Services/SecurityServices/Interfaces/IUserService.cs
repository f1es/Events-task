using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;

namespace Events.Application.Services.SecurityServices.Interfaces;

public interface IUserService
{
    Task<User> GetByIdAsync(Guid id, bool trackChanges);
    Task RegisterUserAsync(UserRegisterRequestDto user, bool trackChanges);
    Task<(string accessToken, RefreshToken refreshToken)> LoginUserAsync(
        UserLoginRequestDto user,
        bool trackUsernameChanges,
        bool trackRefreshTokenChanges);
    Task GrantRole(Guid id, string role, bool trackChanges);
}
