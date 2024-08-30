namespace Events.Application.Usecases.PasswordHasherUsecases.Interfaces;

public interface IGenerateHashUseCase
{
    string GenerateHash(string password);
}
