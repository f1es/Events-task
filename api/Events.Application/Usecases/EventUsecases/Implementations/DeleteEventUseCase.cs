using AutoMapper;
using Events.Application.Usecases.EventUsecases.Interfaces;
using Events.Domain.Exceptions;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;

namespace Events.Application.Usecases.EventUsecases.Implementations;

public class DeleteEventUseCase : IDeleteEventUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IMapper _mapper;
	public DeleteEventUseCase(
		IRepositoryManager repositoryManager,
		IMapper mapper)
    {
        _repositoryManager = repositoryManager;
		_mapper = mapper;
    }
    public async Task DeleteEventAsync(Guid id, bool trackChanges)
    {
		var eventModel = await GetEventByIdAndCheckIfExistAsync(id, trackChanges);

		_repositoryManager.Event.Delete(eventModel);

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
