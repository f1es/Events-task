using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.EventUsecases.Interfaces;

public interface ICreateEventUseCase
{
    Task<EventResponseDto> CreateEventAsync(EventRequestDto eventDto);
}
