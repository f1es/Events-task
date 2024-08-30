using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.EventUsecases.Interfaces;

public interface IGetEventByIdUseCase
{
    Task<EventResponseDto> GetEventByIdAsync(Guid id, bool trackChanges);
}
