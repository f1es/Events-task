using Events.Domain.Models;

namespace Events.Application.Services.Interfaces;

public interface IRefreshTokenService
{
	Task DeleteRefreshTokenAsync(Guid userId, bool trackChanges);
	Task CreateOrUpdateRefreshToken(Guid userId, RefreshToken refreshToken, bool trackChanges);
	Task<RefreshToken> GetRefreshTokenAsync(Guid userId, bool trackChanges);
	Task<RefreshToken> TryGetRefreshTokenAsync(Guid userId, bool trackChanges);
	Task<bool> CompareTokensAsync(Guid userId, string token, bool trackChanges);
}
