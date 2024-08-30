using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.ParticipantUsecases.Interfaces;

public interface IGetParticipantByIdUseCase
{
    Task<ParticipantResponseDto> GetParticipantByIdAsync(Guid eventId, Guid id, bool trackChanges);
}
