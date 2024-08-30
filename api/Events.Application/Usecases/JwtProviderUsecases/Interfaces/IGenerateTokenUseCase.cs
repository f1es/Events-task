using Events.Domain.Models;

namespace Events.Application.Usecases.JwtProviderUsecases.Interfaces;

public interface IGenerateTokenUseCase
{
    string GenerateToken(User user);
}
