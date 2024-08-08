using Events.Domain.Models;

namespace Events.Application.JWT.Interfaces;

public interface IRefreshProvider
{
	RefreshToken GenerateToken(Guid userId);
}
