using AutoMapper;
using Events.Application.Usecases.EventUsecases.Interfaces;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.EventUsecases.Implementations;

public class GetAllEventsUseCase : IGetAllEventsUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IMapper _mapper;
	public GetAllEventsUseCase(
		IRepositoryManager repositoryManager, 
		IMapper mapper)
	{
		_repositoryManager = repositoryManager;
		_mapper = mapper;
	}

	public async Task<IEnumerable<EventResponseDto>> GetAllEventsAsync(EventFilter eventFilter, Paging paging, bool trackChanges)
    {
		var events = await _repositoryManager.Event.GetAllAsync(eventFilter, paging, trackChanges);

		var eventResponse = _mapper.Map<IEnumerable<EventResponseDto>>(events);

		return eventResponse;
	}
}
