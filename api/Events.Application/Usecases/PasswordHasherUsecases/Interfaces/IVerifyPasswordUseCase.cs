namespace Events.Application.Usecases.PasswordHasherUsecases.Interfaces;

public interface IVerifyPasswordUseCase
{
    bool VerifyPassword(string password, string passwordHash);
}
