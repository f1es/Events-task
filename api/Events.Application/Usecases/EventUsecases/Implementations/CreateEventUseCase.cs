using Events.Application.Usecases.EventUsecases.Interfaces;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.EventUsecases.Implementations;

public class CreateEventUseCase : ICreateEventUseCase
{
    public Task<EventResponseDto> CreateEventAsync(EventRequestDto eventDto)
    {
        throw new NotImplementedException();
    }
}
