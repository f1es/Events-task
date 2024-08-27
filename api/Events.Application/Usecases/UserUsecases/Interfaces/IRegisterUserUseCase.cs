using Events.Domain.Shared.DTO.Request;

namespace Events.Application.Usecases.UserUsecases.Interfaces;

public interface IRegisterUserUseCase
{
    Task RegisterUserAsync(UserRegisterRequestDto user, bool trackChanges);
}
