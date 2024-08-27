using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.UserUsecases.Interfaces;

public interface IGetUserByIdUseCase
{
    Task<UserResponseDto> GetUserByIdAsync(Guid id, bool trackChanges);
}
