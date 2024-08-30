using Events.Domain.Models;

namespace Events.Application.Usecases.RefreshTokenUseCase.Interfaces;

public interface IRefreshTokenFromTokenValueUseCase
{
    Task<(string accessToken, RefreshToken refreshToken)> RefreshTokensFromTokenValue(
        string refreshTokenValue,
        bool trackChanges);
}
