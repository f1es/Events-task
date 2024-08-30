using Events.Domain.Models;

namespace Events.Application.Usecases.RefreshProviderUsecase.Interfaces;

public interface IGenerateRefreshTokenUseCase
{
    RefreshToken GenerateToken(Guid userId);
}
