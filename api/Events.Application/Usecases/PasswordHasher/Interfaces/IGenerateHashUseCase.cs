namespace Events.Application.Usecases.PasswordHasher.Interfaces;

public interface IGenerateHashUseCase
{
	string GenerateHash(string password);
}
