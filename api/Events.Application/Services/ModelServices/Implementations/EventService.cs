using AutoMapper;
using Events.Application.Extensions;
using Events.Domain.Repositories.Interfaces;
using Events.Application.Services.ModelServices.Interfaces;
using Events.Domain.Exceptions;
using Events.Domain.Models;
using Events.Domain.Shared;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;
using Events.Domain.Shared.Filters;
using FluentValidation;

namespace Events.Application.Services.ModelServices.Implementations;

public class EventService : IEventService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public EventService(
        IRepositoryManager repositoryManager,
        IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }
    public async Task<EventResponseDto> CreateEventAsync(EventRequestDto eventDto)
    {
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

    public async Task UpdateEventAsync(Guid id, EventRequestDto eventDto, bool trackChanges)
    {
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
