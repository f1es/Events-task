using AutoMapper;
using Events.Application.Usecases.ParticipantUsecases.Extensions;
using Events.Application.Usecases.ParticipantUsecases.Interfaces;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Request;

namespace Events.Application.Usecases.ParticipantUsecases.Implementations;

public class UpdateParticipantUseCase : IUpdateParticipantUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IMapper _mapper;
	public UpdateParticipantUseCase(IRepositoryManager repositoryManager, IMapper mapper)
	{
		_repositoryManager = repositoryManager;
		_mapper = mapper;
	}
	public async Task UpdateParticipantAsync
		(Guid eventId, 
		Guid id, 
		ParticipantRequestDto participant, 
		bool trackChanges)
	{
		var eventModel = await _repositoryManager.GetEventByIdAndCheckIfExistAsync(eventId, trackChanges);

		var participantModel = await _repositoryManager.Participant.GetByIdAsync(id, trackChanges);

		participantModel = _mapper.Map(participant, participantModel);

		await _repositoryManager.SaveAsync();
	}
}
