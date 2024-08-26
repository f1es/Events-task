namespace Events.Application.Usecases.RefreshTokenUseCase.Interfaces;

public interface ICreateRefreshTokenUseCase
{
    Task CreateRefreshTokenAsync(Guid userId);
}
