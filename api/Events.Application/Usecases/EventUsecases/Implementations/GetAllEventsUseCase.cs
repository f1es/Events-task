using Events.Application.Usecases.EventUsecases.Interfaces;
using Events.Domain.Models;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.EventUsecases.Implementations;

public class GetAllEventsUseCase : IGetAllEventsUseCase
{
    public Task<IEnumerable<EventResponseDto>> GetAllEventsAsync(EventFilter eventFilter, Paging paging, bool trackChanges)
    {
        throw new NotImplementedException();
    }
}
