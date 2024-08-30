using Events.Application.Usecases.RefreshProviderUsecase.Interfaces;
using Events.Application.Usecases.RefreshTokenUseCase.Interfaces;
using Events.Domain.Repositories.Interfaces;

namespace Events.Application.Usecases.RefreshTokenUseCase.Implementations;

public class CreateRefreshTokenUseCase : ICreateRefreshTokenUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IGenerateRefreshTokenUseCase _generateRefreshTokenUseCase;

	public CreateRefreshTokenUseCase(IRepositoryManager repositoryManager, IGenerateRefreshTokenUseCase generateRefreshTokenUseCase)
	{
		_repositoryManager = repositoryManager;
		_generateRefreshTokenUseCase = generateRefreshTokenUseCase;
	}

	public async Task CreateRefreshTokenAsync(Guid userId)
	{
		var token = _generateRefreshTokenUseCase.GenerateToken(userId);
		token.Id = Guid.NewGuid();

		_repositoryManager.RefreshToken.Create(token);

		await _repositoryManager.SaveAsync();
	}
}