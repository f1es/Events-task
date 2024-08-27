using Events.Application.Usecases.PasswordHasherUsecases.Interfaces;

namespace Events.Application.Usecases.PasswordHasherUsecases.Implementations;

public class VerifyPasswordUseCase : IVerifyPasswordUseCase
{
	public bool VerifyPassword(string password, string passwordHash) =>
		BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
}
