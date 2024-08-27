using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;

namespace Events.Application.Usecases.UserUsecases.Interfaces;

public interface IGrantRoleForUserUseCase
{
    Task<(string accessToken, RefreshToken refreshToken)> LoginUserAsync(
        UserLoginRequestDto user,
        bool trackUserChanges,
        bool trackRefreshTokenChanges);
}
