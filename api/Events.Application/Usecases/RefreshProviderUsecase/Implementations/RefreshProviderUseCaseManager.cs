using Events.Application.Options;
using Events.Application.Usecases.RefreshProviderUsecase.Interfaces;
using Microsoft.Extensions.Options;

namespace Events.Application.Usecases.RefreshProviderUsecase.Implementations;

public class RefreshProviderUseCaseManager : IRefreshProviderUseCaseManager
{
	private readonly Lazy<IGenerateRefreshTokenUseCase> _generateRefreshTokenUseCase;
	public RefreshProviderUseCaseManager(IOptions<RefreshTokenOptions> refreshTokenOptions)
	{
		_generateRefreshTokenUseCase = new Lazy<IGenerateRefreshTokenUseCase>(() =>
		new GenerateRefreshTokenUseCase(refreshTokenOptions));
	}
	public IGenerateRefreshTokenUseCase GenerateRefreshTokenUseCase => _generateRefreshTokenUseCase.Value;
}
