using AutoMapper;
using Events.Application.Usecases.EventUsecases.Interfaces;
using Events.Domain.Exceptions;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.EventUsecases.Implementations;

public class GetEventByIdUseCase : IGetEventByIdUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IMapper _mapper;
	public GetEventByIdUseCase(
		IRepositoryManager repositoryManager, 
		IMapper mapper)
	{
		_repositoryManager = repositoryManager;
		_mapper = mapper;
	}
	public async Task<EventResponseDto> GetEventByIdAsync(Guid id, bool trackChanges)
	{
		var eventModel = await GetEventByIdAndCheckIfExistAsync(id, trackChanges);

		var eventRsponse = _mapper.Map<EventResponseDto>(eventModel);

		return eventRsponse;
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
