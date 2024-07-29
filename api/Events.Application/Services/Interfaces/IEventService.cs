using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Services.Interfaces;

public interface IEventService
{
	Task<IEnumerable<EventResponseDto>> GetAllEventsAsync(bool trackChanges);
	Task<EventResponseDto> GetEventAsync(Guid id, bool trackChanges);
	Task<EventResponseDto> GetEventByNameAsync(string name, bool trackChanges);
	Task<EventResponseDto> CreateEventAsync(EventForCreateRequestDto eventDto);
	Task UpdateEventAsync(EventForUpdateRequestDto eventDto);
	Task DeleteEventAsync(Guid id);
}
