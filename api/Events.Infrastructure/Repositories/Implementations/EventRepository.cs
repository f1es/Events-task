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

	public async Task<IEnumerable<Event>> GetAllAsync() =>
		await GetAll()
		.ToListAsync();

	public async Task<Event> GetByIdAsync(Guid id) =>
		await GetByPredicate(e => e.Id.Equals(id))
		.SingleOrDefaultAsync();

	public async Task<Event> GetByNameAsync(string name) =>
		await GetByPredicate(e => e.Name.Equals(name))
		.SingleOrDefaultAsync();
}
