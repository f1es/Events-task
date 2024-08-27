namespace Events.Application.Usecases.RefreshTokenUseCase.Interfaces
{
    public interface IRefreshTokenUseCaseManager
    {
        ICreateRefreshTokenUseCase CreateRefreshTokenUseCase { get; }
        IRefreshTokenFromTokenValueUseCase RefreshTokenFromTokenValueUseCase { get; }
        IUpdateRefreshTokenUseCase UpdateRefreshTokenUseCase { get; }
    }
}