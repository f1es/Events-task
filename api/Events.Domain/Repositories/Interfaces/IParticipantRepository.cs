using Events.Domain.Models;

namespace Events.Domain.Repositories.Interfaces;

public interface IParticipantRepository : IBaseRepository<Participant>
{
	Task<IEnumerable<Participant>> GetAllAsync(Guid eventId, Paging paging, bool trackChanges);
	Task<Participant> GetByIdAsync(Guid id, bool trackChanges);
}
