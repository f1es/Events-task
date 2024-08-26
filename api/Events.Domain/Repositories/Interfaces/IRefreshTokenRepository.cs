using Events.Domain.Models;

namespace Events.Domain.Repositories.Interfaces;

public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
{
	Task<RefreshToken> GetRefreshTokenByUserIdAsync(Guid userId, bool trackChanges);
	Task<RefreshToken> GetRefreshTokenByValueAsync(string refreshTokenValue, bool trackChanges);
}
