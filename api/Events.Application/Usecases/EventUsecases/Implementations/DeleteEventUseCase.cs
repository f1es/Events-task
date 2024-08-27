using AutoMapper;
using Events.Application.Usecases.EventUsecases.Extensions;
using Events.Application.Usecases.EventUsecases.Interfaces;
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
		var eventModel = await _repositoryManager.GetEventByIdAndCheckIfExistAsync(id, trackChanges);

		_repositoryManager.Event.Delete(eventModel);

		await _repositoryManager.SaveAsync();
	}
}
