using AutoMapper;
using Events.Application.Usecases.EventUsecases.Extensions;
using Events.Application.Usecases.EventUsecases.Interfaces;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Request;

namespace Events.Application.Usecases.EventUsecases.Implementations;

public class UpdateEventUseCase : IUpdateEventUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IMapper _mapper;

	public UpdateEventUseCase(
		IRepositoryManager repositoryManager, 
		IMapper mapper)
	{
		_repositoryManager = repositoryManager;
		_mapper = mapper;
	}

	public async Task UpdateEventAsync(Guid id, EventRequestDto eventDto, bool trackChanges)
    {
		var eventModel = await _repositoryManager.GetEventByIdAndCheckIfExistAsync(id, trackChanges);

		eventModel = _mapper.Map(eventDto, eventModel);

		await _repositoryManager.SaveAsync();
	}
}
