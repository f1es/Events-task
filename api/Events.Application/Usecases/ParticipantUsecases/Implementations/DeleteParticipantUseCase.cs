using AutoMapper;
using Events.Application.Usecases.ParticipantUsecases.Extensions;
using Events.Application.Usecases.ParticipantUsecases.Interfaces;
using Events.Domain.Repositories.Interfaces;

namespace Events.Application.Usecases.ParticipantUsecases.Implementations;

public class DeleteParticipantUseCase : IDeleteParticipantUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	public DeleteParticipantUseCase(IRepositoryManager repositoryManager)
	{
		_repositoryManager = repositoryManager;
	}
	public async Task DeleteParticipantAsync(Guid eventId, Guid id, bool trackChanges)
	{
		var eventModel = await _repositoryManager.GetEventByIdAndCheckIfExistAsync(eventId, trackChanges);

		var participantModel = await _repositoryManager.GetParticipantByIdAndCheckIfExistAsync(eventId, id, trackChanges);

		_repositoryManager.Participant.Delete(participantModel);

		await _repositoryManager.SaveAsync();
	}
}
