using Events.Domain.Models;
using Events.Domain.Shared;
using Events.Domain.Shared.Filters;

namespace Events.Domain.Repositories.Interfaces;

public interface IEventRepository
{
	Task<IEnumerable<Event>> GetAllAsync(EventFilter eventFilter, Paging paging, bool trackChanges);
	Task<Event> GetByIdAsync(Guid id, bool trackChanges);
	void CreateEvent(Event eventModel);
	void UpdateEvent(Event eventModel);
	void DeleteEvent(Event eventModel);
}
