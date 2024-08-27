using AutoMapper;
using Events.Application.Usecases.EventUsecases.Extensions;
using Events.Application.Usecases.EventUsecases.Interfaces;
using Events.Domain.Repositories.Interfaces;

namespace Events.Application.Usecases.EventUsecases.Implementations;

public class DeleteEventUseCase : IDeleteEventUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	public DeleteEventUseCase(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    public async Task DeleteEventAsync(Guid id, bool trackChanges)
    {
		var eventModel = await _repositoryManager.GetEventByIdAndCheckIfExistAsync(id, trackChanges);

		_repositoryManager.Event.Delete(eventModel);

		await _repositoryManager.SaveAsync();
	}
}
