using Events.Application.Usecases.PasswordHasherUsecases.Interfaces;

namespace Events.Application.Usecases.PasswordHasherUsecases.Implementations;

public class GenerateHashUseCase : IGenerateHashUseCase
{
	public string GenerateHash(string password) =>
		BCrypt.Net.BCrypt.EnhancedHashPassword(password);
}
