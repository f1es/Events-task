using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.ParticipantUsecases.Interfaces;

public interface ICreateParticipantUseCase
{
    Task<ParticipantResponseDto> CreateParticipantAsync(
        Guid eventId,
        Guid userId,
        ParticipantRequestDto participant,
        bool trackChanges);
}
