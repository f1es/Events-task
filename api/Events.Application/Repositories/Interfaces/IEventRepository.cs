using Events.Domain.Models;

namespace Events.Application.Repositories.Interfaces;

public interface IEventRepository : IRepositoryBase<Event>
{
	Task<IEnumerable<Event>> GetAllAsync();
	Task<Event> GetByIdAsync(Guid id);
	Task<Event> GetByNameAsync(string name);
}
