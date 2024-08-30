using Events.Application.Usecases.PasswordHasherUsecases.Interfaces;

namespace Events.Application.Usecases.PasswordHasherUsecases.Implementations;

public class PasswordHasherUseCaseManager : IPasswordHasherUseCaseManager
{
	private readonly Lazy<IGenerateHashUseCase> _generateHashUseCase;
	private readonly Lazy<IVerifyPasswordUseCase> _verifyPasswordUseCase;
	public PasswordHasherUseCaseManager()
	{
		_generateHashUseCase = new Lazy<IGenerateHashUseCase>(() =>
		new GenerateHashUseCase());

		_verifyPasswordUseCase = new Lazy<IVerifyPasswordUseCase>(() =>
		new VerifyPasswordUseCase());
	}

	public IGenerateHashUseCase GenerateHashUseCase => _generateHashUseCase.Value;

	public IVerifyPasswordUseCase VerifyPasswordUseCase => _verifyPasswordUseCase.Value;
}
