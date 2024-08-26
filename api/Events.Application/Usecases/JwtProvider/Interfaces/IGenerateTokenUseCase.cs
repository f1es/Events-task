using Events.Domain.Models;

namespace Events.Application.Usecases.JwtProvider.Interfaces;

public interface IGenerateTokenUseCase
{
	string GenerateToken(User user);
}
