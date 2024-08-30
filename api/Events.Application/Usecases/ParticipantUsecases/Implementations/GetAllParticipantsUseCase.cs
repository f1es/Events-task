using AutoMapper;
using Events.Application.Usecases.ParticipantUsecases.Extensions;
using Events.Application.Usecases.ParticipantUsecases.Interfaces;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.ParticipantUsecases.Implementations;

public class GetAllParticipantsUseCase : IGetAllParticipantsUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IMapper _mapper;
	public GetAllParticipantsUseCase(
		IRepositoryManager repositoryManager,
		IMapper mapper)
	{
		_repositoryManager = repositoryManager;
		_mapper = mapper;
	}
	public async Task<IEnumerable<ParticipantResponseDto>> GetAllParticipantsAsync(Guid eventId, Paging paging, bool trackChanges)
	{
		var eventModel = await _repositoryManager.GetEventByIdAndCheckIfExistAsync(eventId, trackChanges);

		var participantsModels = await _repositoryManager.Participant.GetAllAsync(eventId, paging, trackChanges);

		var participantsResponses = _mapper.Map<IEnumerable<ParticipantResponseDto>>(participantsModels);

		return participantsResponses;
	}
}
