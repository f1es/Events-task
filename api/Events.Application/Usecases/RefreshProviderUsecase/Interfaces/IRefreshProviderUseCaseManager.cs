namespace Events.Application.Usecases.RefreshProviderUsecase.Interfaces
{
    public interface IRefreshProviderUseCaseManager
    {
        IGenerateRefreshTokenUseCase GenerateRefreshTokenUseCase { get; }
    }
}