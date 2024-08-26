using Events.Domain.Models;

namespace Events.Domain.Repositories.Interfaces;

public interface IEventRepository : IBaseRepository<Event>
{
	Task<IEnumerable<Event>> GetAllAsync(EventFilter eventFilter, Paging paging, bool trackChanges);
	Task<Event> GetByIdAsync(Guid id, bool trackChanges);
}
