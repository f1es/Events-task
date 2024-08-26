using Events.Domain.Models;

namespace Events.Domain.Repositories.Interfaces;

public interface IEventRepository
{
	Task<IEnumerable<Event>> GetAllAsync(EventFilter eventFilter, Paging paging, bool trackChanges);
	Task<Event> GetByIdAsync(Guid id, bool trackChanges);
	void CreateEvent(Event eventModel);
	void UpdateEvent(Event eventModel);
	void DeleteEvent(Event eventModel);
}
