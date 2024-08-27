namespace Events.Application.Usecases.JwtProviderUsecases.Interfaces
{
    public interface IJwtProviderUseCaseManager
    {
        IGenerateTokenUseCase GenerateTokenUseCase { get; }
        IGetUserIdUseCase GetUserIdUseCase { get; }
    }
}