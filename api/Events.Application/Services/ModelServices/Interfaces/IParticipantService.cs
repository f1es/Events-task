using Events.Domain.Models;
using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Services.ModelServices.Interfaces;

public interface IParticipantService
{
    Task<ParticipantResponseDto> CreateParticipantAsync(
        Guid eventId,
        Guid userId,
		ParticipantRequestDto participant,
        bool trackChanges);
    Task<IEnumerable<ParticipantResponseDto>> GetAllParticipantsAsync(Guid eventId, Paging paging, bool trackChanges);
    Task<ParticipantResponseDto> GetParticipantByIdAsync(Guid eventId, Guid id, bool trackChanges);
    Task DeleteParticipantAsync(Guid eventId, Guid id, bool trackChanges);
    Task UpdateParticipantAsync(
        Guid eventId,
        Guid id,
		ParticipantRequestDto participant,
        bool trackChanges);
}
