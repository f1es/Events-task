using AutoMapper;
using Events.Application.Usecases.ParticipantUsecases.Extensions;
using Events.Application.Usecases.ParticipantUsecases.Interfaces;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.ParticipantUsecases.Implementations;

public class CreateParticipantUseCase : ICreateParticipantUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IMapper _mapper;

	public CreateParticipantUseCase(
		IRepositoryManager repositoryManager, 
		IMapper mapper)
	{
		_repositoryManager = repositoryManager;
		_mapper = mapper;
	}

	public async Task<ParticipantResponseDto> CreateParticipantAsync(
		Guid eventId, 
		Guid userId, 
		ParticipantRequestDto participant, 
		bool trackChanges)
	{
		var eventModel = await _repositoryManager.GetEventByIdAndCheckIfExistAsync(eventId, trackChanges);
		var userModel = await _repositoryManager.GetUserByIdAndCheckIfExistAsync(userId, trackChanges);

		var participantModel = _mapper.Map<Participant>(participant);
		participantModel.EventId = eventId;
		participantModel.UserId = userId;

		_repositoryManager.Participant.Create(participantModel);

		await _repositoryManager.SaveAsync();

		var participantResponse = _mapper.Map<ParticipantResponseDto>(participantModel);

		return participantResponse;
	}
}
