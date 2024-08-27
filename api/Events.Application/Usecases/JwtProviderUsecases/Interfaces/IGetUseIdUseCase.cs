namespace Events.Application.Usecases.JwtProviderUsecases.Interfaces;

public interface IGetUseIdUseCase
{
    Guid GetUserId(string token);
}
