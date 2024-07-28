using Events.Domain.Models;

namespace Events.Application.Repositories.Interfaces;

public interface IEventRepository : IRepositoryBase<Event>
{
	Task<IEnumerable<Event>> GetAllAsync(bool trackChanges);
	Task<Event> GetByIdAsync(Guid id, bool trackChanges);
	Task<Event> GetByNameAsync(string name, bool trackChanges);
}
