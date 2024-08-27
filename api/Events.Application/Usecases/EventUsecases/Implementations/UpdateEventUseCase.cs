using Events.Application.Usecases.EventUsecases.Interfaces;
using Events.Domain.Shared.DTO.Request;

namespace Events.Application.Usecases.EventUsecases.Implementations;

public class UpdateEventUseCase : IUpdateEventUseCase
{
    public Task UpdateEventAsync(Guid id, EventRequestDto eventDto, bool trackChanges)
    {
        throw new NotImplementedException();
    }
}
