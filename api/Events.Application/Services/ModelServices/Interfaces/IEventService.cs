﻿using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Services.ModelServices.Interfaces;

public interface IEventService
{
    Task<IEnumerable<EventResponseDto>> GetAllEventsAsync(EventFilter eventFilter, Paging paging, bool trackChanges);
    Task<EventResponseDto> GetEventByIdAsync(Guid id, bool trackChanges);
    Task<EventResponseDto> CreateEventAsync(EventRequestDto eventDto);
    Task UpdateEventAsync(Guid id, EventRequestDto eventDto, bool trackChanges);
    Task DeleteEventAsync(Guid id, bool trackChanges);
}
