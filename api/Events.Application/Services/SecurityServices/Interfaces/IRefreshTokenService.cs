using Events.Domain.Models;

namespace Events.Application.Services.SecurityServices.Interfaces;

public interface IRefreshTokenService
{
    Task CreateRefreshTokenAsync(Guid userId);
    Task UpdateRefreshToken(Guid userId, RefreshToken refreshToken, bool trackChanges);
    Task<(string accessToken, RefreshToken refreshToken)> RefreshTokensFromTokenValue(
        string refreshTokenValue,
        bool trackChanges);
}
