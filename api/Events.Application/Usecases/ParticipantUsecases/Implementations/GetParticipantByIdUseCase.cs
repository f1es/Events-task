using AutoMapper;
using Events.Application.Usecases.ParticipantUsecases.Extensions;
using Events.Application.Usecases.ParticipantUsecases.Interfaces;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.ParticipantUsecases.Implementations;

public class GetParticipantByIdUseCase : IGetParticipantByIdUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IMapper _mapper;
	public GetParticipantByIdUseCase(
		IRepositoryManager repositoryManager, 
		IMapper mapper)
	{
		_repositoryManager = repositoryManager;
		_mapper = mapper;
	}
	public async Task<ParticipantResponseDto> GetParticipantByIdAsync(Guid eventId, Guid id, bool trackChanges)
	{
		var eventModel = await _repositoryManager.GetEventByIdAndCheckIfExistAsync(eventId, trackChanges);

		var participantModel = await _repositoryManager.GetParticipantByIdAndCheckIfExistAsync(eventId, id, trackChanges);

		var participantResponse = _mapper.Map<ParticipantResponseDto>(participantModel);

		return participantResponse;
	}
}
