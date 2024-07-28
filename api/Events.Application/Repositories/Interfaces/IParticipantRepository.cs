using Events.Domain.Models;

namespace Events.Application.Repositories.Interfaces;

public interface IParticipantRepository : IRepositoryBase<Participant>
{
	Task<IEnumerable<Participant>> GetAllAsync();
	Task<Participant> GetByIdAsync(Guid eventId, Guid id);
}
