using Events.Domain.Models;

namespace Events.Domain.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
	Task<RefreshToken> GetRefreshTokenByUserIdAsync(Guid userId, bool trackChanges);
	void CreateRefreshToken(RefreshToken refreshToken);
	void DeleteRefreshToken(RefreshToken refreshToken);
	Task<RefreshToken> GetRefreshTokenByValueAsync(string refreshTokenValue, bool trackChanges);
}
