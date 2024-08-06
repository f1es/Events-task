using Events.Domain.Shared.DTO.Request;
using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Services.Interfaces;

public interface IParticipantService
{
	Task<ParticipantResponseDto> CreateParticipantAsync(
		Guid eventId, 
		Guid userId, 
		ParticipantForCreateRequestDto participant, 
		bool trackChanges);
	Task<IEnumerable<ParticipantResponseDto>> GetAllParticipantsAsync(Guid eventId, bool trackChanges);
	Task<ParticipantResponseDto> GetParticipantByIdAsync(Guid eventId, Guid id, bool trackChanges);
	Task DeleteParticipantAsync(Guid eventId, Guid id, bool trackChanges);
	Task UpdateParticipantAsync(Guid eventId, Guid id, ParticipantForUpdateRequestDto participant, bool trackChanges);
}
