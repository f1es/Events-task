using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.Participant.Interfaces;

public interface IGetParticipantByIdUseCase
{
	Task<ParticipantResponseDto> GetParticipantByIdAsync(Guid eventId, Guid id, bool trackChanges);
}
