using Events.Domain.Models;

namespace Events.Application.Usecases.RefreshProvider.Interfaces;

public interface IGenerateRefreshTokenUseCase
{
	RefreshToken GenerateToken(Guid userId);
}
