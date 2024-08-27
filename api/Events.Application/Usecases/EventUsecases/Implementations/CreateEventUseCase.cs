using AutoMapper;
using Events.Application.Usecases.EventUsecases.Interfaces;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.EventUsecases.Implementations;

public class CreateEventUseCase : ICreateEventUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IMapper _mapper;
    public CreateEventUseCase(
		IRepositoryManager repositoryManager,
		IMapper mapper)
    {
		_repositoryManager = repositoryManager;
        _mapper = mapper;
    }
    public async Task<EventResponseDto> CreateEventAsync(EventRequestDto eventDto)
    {
		var mappedEvent = _mapper.Map<Event>(eventDto);

		_repositoryManager.Event.Create(mappedEvent);

		await _repositoryManager.SaveAsync();

		var eventResponse = _mapper.Map<EventResponseDto>(mappedEvent);
		return eventResponse;
	}
}
