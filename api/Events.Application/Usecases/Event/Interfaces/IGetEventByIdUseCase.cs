using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.Event.Interfaces;

public interface IGetEventByIdUseCase
{
	Task<EventResponseDto> GetEventByIdAsync(Guid id, bool trackChanges);
}
