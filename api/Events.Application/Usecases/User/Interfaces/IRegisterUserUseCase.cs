using Events.Domain.Shared.DTO.Request;

namespace Events.Application.Usecases.User.Interfaces;

public interface IRegisterUserUseCase
{
	Task RegisterUserAsync(UserRegisterRequestDto user, bool trackChanges);
}
