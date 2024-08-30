using Events.Domain.Shared.DTO.Request;

namespace Events.Application.Usecases.EventUsecases.Interfaces;

public interface IUpdateEventUseCase
{
    Task UpdateEventAsync(Guid id, EventRequestDto eventDto, bool trackChanges);
}
