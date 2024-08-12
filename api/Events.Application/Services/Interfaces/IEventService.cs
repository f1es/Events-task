using Events.Domain.Shared;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;
using Events.Domain.Shared.Filters;

namespace Events.Application.Services.Interfaces;

public interface IEventService
{
	Task<IEnumerable<EventResponseDto>> GetAllEventsAsync(EventFilter eventFilter, Paging paging, bool trackChanges);
	Task<EventResponseDto> GetEventByIdAsync(Guid id, bool trackChanges);
	Task<EventResponseDto> CreateEventAsync(EventForCreateRequestDto eventDto);
	Task UpdateEventAsync(Guid id, EventForUpdateRequestDto eventDto, bool trackChanges);
	Task DeleteEventAsync(Guid id, bool trackChanges);
}
