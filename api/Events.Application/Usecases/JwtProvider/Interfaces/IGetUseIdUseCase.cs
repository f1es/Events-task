namespace Events.Application.Usecases.JwtProvider.Interfaces;

public interface IGetUseIdUseCase
{
	Guid GetUserId(string token);
}
