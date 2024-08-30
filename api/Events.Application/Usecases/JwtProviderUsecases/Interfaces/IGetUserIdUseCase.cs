namespace Events.Application.Usecases.JwtProviderUsecases.Interfaces;

public interface IGetUserIdUseCase
{
    Guid GetUserId(string token);
}
