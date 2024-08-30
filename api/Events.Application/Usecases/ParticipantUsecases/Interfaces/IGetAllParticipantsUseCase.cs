using Events.Domain.Models;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.ParticipantUsecases.Interfaces;

public interface IGetAllParticipantsUseCase
{
    Task<IEnumerable<ParticipantResponseDto>> GetAllParticipantsAsync(Guid eventId, Paging paging, bool trackChanges);
}
