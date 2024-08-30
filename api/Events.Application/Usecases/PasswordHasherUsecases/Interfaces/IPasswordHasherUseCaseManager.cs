namespace Events.Application.Usecases.PasswordHasherUsecases.Interfaces
{
    public interface IPasswordHasherUseCaseManager
    {
        IGenerateHashUseCase GenerateHashUseCase { get; }
        IVerifyPasswordUseCase VerifyPasswordUseCase { get; }
    }
}