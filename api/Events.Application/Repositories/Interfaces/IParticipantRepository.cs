using Events.Domain.Models;

namespace Events.Application.Repositories.Interfaces;

public interface IParticipantRepository
{
	Task<IEnumerable<Participant>> GetAllAsync(Guid eventId, bool trackChanges);
	Task<Participant> GetByIdAsync(Guid eventId, Guid id, bool trackChanges);
	void CreateParticipant(Guid eventId, Guid userID, Participant participant);
	void DeleteParticipant(Participant participant);
	void UpdateParticipant(Participant participant);
}
