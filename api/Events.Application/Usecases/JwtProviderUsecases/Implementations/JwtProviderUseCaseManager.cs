using Events.Application.Options;
using Events.Application.Usecases.JwtProviderUsecases.Interfaces;
using Microsoft.Extensions.Options;

namespace Events.Application.Usecases.JwtProviderUsecases.Implementations;

public class JwtProviderUseCaseManager : IJwtProviderUseCaseManager
{
	private readonly Lazy<IGenerateTokenUseCase> generateTokenUseCase;
	private readonly Lazy<IGetUserIdUseCase> getUserIdUseCase;
	public JwtProviderUseCaseManager(IOptions<JwtOptions> jwtOptions)
	{
		generateTokenUseCase = new Lazy<IGenerateTokenUseCase>(() =>
		new GenerateTokenUseCase(jwtOptions));

		getUserIdUseCase = new Lazy<IGetUserIdUseCase>(() =>
		new GetUserIdUseCase());
	}

	public IGenerateTokenUseCase GenerateTokenUseCase => generateTokenUseCase.Value;

	public IGetUserIdUseCase GetUserIdUseCase => getUserIdUseCase.Value;
}
