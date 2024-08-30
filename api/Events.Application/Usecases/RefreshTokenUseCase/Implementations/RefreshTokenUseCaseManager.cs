using AutoMapper;
using Events.Application.Usecases.JwtProviderUsecases.Interfaces;
using Events.Application.Usecases.RefreshProviderUsecase.Interfaces;
using Events.Application.Usecases.RefreshTokenUseCase.Interfaces;
using Events.Domain.Repositories.Interfaces;

namespace Events.Application.Usecases.RefreshTokenUseCase.Implementations;

public class RefreshTokenUseCaseManager : IRefreshTokenUseCaseManager
{
	private readonly Lazy<ICreateRefreshTokenUseCase> _createRefreshTokenUseCase;
	private readonly Lazy<IRefreshTokenFromTokenValueUseCase> _refreshTokenFromTokenValueUseCase;
	private readonly Lazy<IUpdateRefreshTokenUseCase> _updateRefreshTokenUseCase;

	public RefreshTokenUseCaseManager(
		IRepositoryManager repositoryManager,
		IRefreshProviderUseCaseManager refreshProviderUseCaseManager,
		IJwtProviderUseCaseManager jwtProviderUseCaseManager,
		IMapper mapper)
	{
		_createRefreshTokenUseCase = new Lazy<ICreateRefreshTokenUseCase>(() =>
		new CreateRefreshTokenUseCase(
			repositoryManager,
			refreshProviderUseCaseManager.GenerateRefreshTokenUseCase));

		_refreshTokenFromTokenValueUseCase = new Lazy<IRefreshTokenFromTokenValueUseCase>(() =>
		new RefreshTokenFromTokenValueUseCase(
			repositoryManager,
			UpdateRefreshTokenUseCase,
			refreshProviderUseCaseManager.GenerateRefreshTokenUseCase,
			jwtProviderUseCaseManager.GenerateTokenUseCase));

		_updateRefreshTokenUseCase = new Lazy<IUpdateRefreshTokenUseCase>(() =>
		new UpdateRefreshTokenUseCase(
			repositoryManager,
			mapper));
	}

	public ICreateRefreshTokenUseCase CreateRefreshTokenUseCase => _createRefreshTokenUseCase.Value;
	public IRefreshTokenFromTokenValueUseCase RefreshTokenFromTokenValueUseCase => _refreshTokenFromTokenValueUseCase.Value;
	public IUpdateRefreshTokenUseCase UpdateRefreshTokenUseCase => _updateRefreshTokenUseCase.Value;
}
