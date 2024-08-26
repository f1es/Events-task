namespace Events.Application.Usecases.PasswordHasher.Interfaces;

public interface IVerifyPasswordUseCase
{
	bool VerifyPassword(string password, string passwordHash);
}
