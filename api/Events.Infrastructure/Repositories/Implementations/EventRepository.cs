using Events.Application.Repositories.Interfaces;
using Events.Domain.Models;
using Events.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Repositories.Implementations;

public class EventRepository : BaseRepository<Event>, IEventRepository
{
    public EventRepository(EventsDBContext eventsDBContext)
        : base(eventsDBContext)
    {
        
    }

	public async Task<IEnumerable<Event>> GetAllAsync(bool trackChanges) =>
		await GetAll(trackChanges)
		.ToListAsync();

	public async Task<Event> GetByIdAsync(Guid id, bool trackChanges) =>
		await GetByPredicate(e => e.Id.Equals(id), trackChanges)
		.SingleOrDefaultAsync();

	public async Task<Event> GetByNameAsync(string name, bool trackChanges) =>
		await GetByPredicate(e => e.Name.Equals(name), trackChanges)
		.SingleOrDefaultAsync();

	public void CreateEvent(Event eventModel) => 
		Create(eventModel);

	public void UpdateEvent(Event eventModel) =>
		Update(eventModel);

	public void DeleteEvent(Event eventModel) =>
		Delete(eventModel);
}
