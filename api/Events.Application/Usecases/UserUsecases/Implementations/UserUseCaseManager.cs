using AutoMapper;
using Events.Application.Usecases.JwtProviderUsecases.Interfaces;
using Events.Application.Usecases.PasswordHasherUsecases.Interfaces;
using Events.Application.Usecases.RefreshProviderUsecase.Interfaces;
using Events.Application.Usecases.RefreshTokenUseCase.Interfaces;
using Events.Application.Usecases.UserUsecases.Interfaces;
using Events.Domain.Repositories.Interfaces;

namespace Events.Application.Usecases.UserUsecases.Implementations;

public class UserUseCaseManager : IUserUseCaseManager
{
	private readonly Lazy<IGetAllUsersUseCase> _getAllUsersUseCase;
	private readonly Lazy<IGetUserByIdUseCase> _getUserByIdUseCase;
	private readonly Lazy<IGrantRoleForUserUseCase> _grantRoleForUserUseCase;
	private readonly Lazy<ILoginUserUseCase> _loginUserUseCase;
	private readonly Lazy<IRegisterUserUseCase> _registerUserUseCase;

	public UserUseCaseManager(
		IRepositoryManager repositoryManager,
		IMapper mapper,
		IPasswordHasherUseCaseManager passwordHasherUseCaseManager,
		IJwtProviderUseCaseManager jwtProviderUseCaseManager,
		IRefreshProviderUseCaseManager refreshProviderUseCaseManager,
		IRefreshTokenUseCaseManager refreshTokenUseCaseManager)
	{
		_getAllUsersUseCase = new Lazy<IGetAllUsersUseCase>(() =>
		new GetAllUsersUseCase(
			repositoryManager,
			mapper));

		_getUserByIdUseCase = new Lazy<IGetUserByIdUseCase>(() =>
		new GetUserByIdUseCase(
			repositoryManager,
			mapper));

		_grantRoleForUserUseCase = new Lazy<IGrantRoleForUserUseCase>(() =>
		new GrantRoleForUserUseCase(repositoryManager));

		_loginUserUseCase = new Lazy<ILoginUserUseCase>(() =>
		new LoginUserUseCase(
			repositoryManager,
			passwordHasherUseCaseManager.VerifyPasswordUseCase,
			jwtProviderUseCaseManager.GenerateTokenUseCase,
			refreshProviderUseCaseManager.GenerateRefreshTokenUseCase,
			refreshTokenUseCaseManager.UpdateRefreshTokenUseCase));

		_registerUserUseCase = new Lazy<IRegisterUserUseCase>(() =>
		new RegisterUserUseCase(
			repositoryManager,
			mapper,
			refreshTokenUseCaseManager.CreateRefreshTokenUseCase,
			passwordHasherUseCaseManager.GenerateHashUseCase));
	}

	public IGetAllUsersUseCase GetAllUsersUseCase => _getAllUsersUseCase.Value;

	public IGetUserByIdUseCase GetUserByIdUseCase => _getUserByIdUseCase.Value;

	public IGrantRoleForUserUseCase GrantRoleForUserUseCase => _grantRoleForUserUseCase.Value;

	public ILoginUserUseCase LoginUserUseCase => _loginUserUseCase.Value;

	public IRegisterUserUseCase RegisterUserUseCase => _registerUserUseCase.Value;
}
