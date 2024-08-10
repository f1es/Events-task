using Events.Domain.Models;
using Events.Domain.Shared;

namespace Events.Application.Repositories.Interfaces;

public interface IParticipantRepository
{
	Task<IEnumerable<Participant>> GetAllAsync(Guid eventId, Paging paging, bool trackChanges);
	Task<Participant> GetByIdAsync(Guid id, bool trackChanges);
	void CreateParticipant(Guid eventId, Guid userID, Participant participant);
	void DeleteParticipant(Participant participant);
	void UpdateParticipant(Participant participant);
}
