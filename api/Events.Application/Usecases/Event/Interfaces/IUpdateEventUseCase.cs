using Events.Domain.Shared.DTO.Request;

namespace Events.Application.Usecases.Event.Interfaces;

public interface IUpdateEventUseCase
{
	Task UpdateEventAsync(Guid id, EventRequestDto eventDto, bool trackChanges);
}
