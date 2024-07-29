using Events.Domain.Models;

namespace Events.Application.Repositories.Interfaces;

public interface IEventRepository
{
	Task<IEnumerable<Event>> GetAllAsync(bool trackChanges);
	Task<Event> GetByIdAsync(Guid id, bool trackChanges);
	Task<Event> GetByNameAsync(string name, bool trackChanges);
	void CreateEvent(Event eventModel);
	void UpdateEvent(Event eventModel);
	void DeleteEvent(Event eventModel);
}
