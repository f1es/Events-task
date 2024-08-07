using AutoMapper;
using Events.Application.Extensions;
using Events.Application.Repositories.Interfaces;
using Events.Application.Services.Interfaces;
using Events.Application.Validators;
using Events.Domain.Exceptions;
using Events.Domain.Models;
using Events.Domain.Shared;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;
using Events.Domain.Shared.Filters;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Events.Application.Services.Implementations;

public class EventService : IEventService
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IMapper _mapper;
	private readonly IValidator<EventForCreateRequestDto> _createValidator;
	private readonly IValidator<EventForUpdateRequestDto> _updateValidator;
    public EventService(
		IRepositoryManager repositoryManager, 
		IMapper mapper,
		IValidator<EventForCreateRequestDto> createValidator,
		IValidator<EventForUpdateRequestDto> updateValidator)
    {
		_repositoryManager = repositoryManager;
		_mapper = mapper;
		_createValidator = createValidator;
		_updateValidator = updateValidator;
    }
    public async Task<EventResponseDto> CreateEventAsync(EventForCreateRequestDto eventDto)
	{
		var validationResult = await _createValidator.ValidateAsync(eventDto);

		if (!validationResult.IsValid)
		{
			throw new InvalidModelException(validationResult.GetMessage());
		}

		var mappedEvent = _mapper.Map<Event>(eventDto);

		_repositoryManager.Event.CreateEvent(mappedEvent);

		await _repositoryManager.SaveAsync();

		var eventResponse = _mapper.Map<EventResponseDto>(mappedEvent);
		return eventResponse;
	}

	public async Task DeleteEventAsync(Guid id, bool trackChanges)
	{
		var eventModel = await GetEventByIdAndCheckIfExistAsync(id, trackChanges);

		_repositoryManager.Event.DeleteEvent(eventModel);

		await _repositoryManager.SaveAsync();
	}

	public async Task<IEnumerable<EventResponseDto>> GetAllEventsAsync(EventFilter eventFilter, Paging paging, bool trackChanges)
	{
		var events = await _repositoryManager.Event.GetAllAsync(eventFilter, paging, trackChanges);

		var eventResponse = _mapper.Map<IEnumerable<EventResponseDto>>(events);

		return eventResponse;
	}

	public async Task<EventResponseDto> GetEventByIdAsync(Guid id, bool trackChanges)
	{
		var eventModel = await GetEventByIdAndCheckIfExistAsync(id, trackChanges);

		var eventRsponse = _mapper.Map<EventResponseDto>(eventModel);

		return eventRsponse;
	}

	public async Task<EventResponseDto> GetEventByNameAsync(string name, bool trackChanges)
	{
		var eventModel = await _repositoryManager.Event.GetByNameAsync(name, trackChanges);

		if (eventModel is null)
		{
			throw new NotFoundException($"event with name {name} not found");
		}

		var eventResponse = _mapper.Map<EventResponseDto>(eventModel);

		return eventResponse;
	}

	public async Task UpdateEventAsync(Guid id, EventForUpdateRequestDto eventDto, bool trackChanges)
	{
		var validationResult = await _updateValidator.ValidateAsync(eventDto);

		if (!validationResult.IsValid)
		{
			throw new InvalidModelException(validationResult.GetMessage());
		}

		var eventModel = await GetEventByIdAndCheckIfExistAsync(id, trackChanges);

		eventModel = _mapper.Map(eventDto, eventModel);

		await _repositoryManager.SaveAsync();
	}

	private async Task<Event> GetEventByIdAndCheckIfExistAsync(Guid id, bool trackChanges)
	{
		var eventModel = await _repositoryManager.Event.GetByIdAsync(id, trackChanges);

		if (eventModel is null)
		{
			throw new NotFoundException($"event with id {id} not found");
		}

		return eventModel;
	}
}
