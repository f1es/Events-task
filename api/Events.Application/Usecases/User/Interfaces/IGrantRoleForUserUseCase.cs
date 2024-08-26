using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;

namespace Events.Application.Usecases.User.Interfaces;

public interface IGrantRoleForUserUseCase
{
	Task<(string accessToken, RefreshToken refreshToken)> LoginUserAsync(
		UserLoginRequestDto user,
		bool trackUserChanges,
		bool trackRefreshTokenChanges);
}
