﻿using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.Event.Interfaces;

public interface ICreateEventUseCase
{
	Task<EventResponseDto> CreateEventAsync(EventRequestDto eventDto);
}
