using Events.Domain.Repositories.Interfaces;
using Events.Domain.Models;
using Events.Infrastructure.Context;
using Events.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Events.Infrastructure.Repositories.Implementations;

public class EventRepository : BaseRepository<Event>, IEventRepository
{
    public EventRepository(EventsDBContext eventsDBContext)
        : base(eventsDBContext)
    {
        
    }

	public async Task<IEnumerable<Event>> GetAllAsync(
		EventFilter eventFilter, 
		Paging paging,
		bool trackChanges)
	{
		var eventsQuery = GetAll(trackChanges);

		eventsQuery = SearchByName(eventsQuery, eventFilter.Search);

		eventsQuery = Sort(eventsQuery, eventFilter.SortItem, eventFilter.SortOrder);

		eventsQuery = eventsQuery.Paginate(paging.Page, paging.PageSize);

		return await eventsQuery.ToListAsync();
	}
	private IQueryable<Event> SearchByName(IQueryable<Event> events, string? name)
	{
		events = events.Where(s => string.IsNullOrEmpty(name) ||
			s.Name.ToLower().Contains(name.ToLower()));

		return events;
	}
	private IQueryable<Event> Sort(IQueryable<Event> events, string? sortItem, string? sortOrder)
	{
		if (sortOrder == "desc")
			events = events.OrderByDescending(GetKeySelector(sortItem));
		else
			events = events.OrderBy(GetKeySelector(sortItem));

		return events;
	}
	private Expression<Func<Event, object>> GetKeySelector(string? sortItem)
	{
		Expression<Func<Event, object>> keySelector = sortItem switch
		{
			"date" => e => e.Date,
			"name" => e => e.Name,
			"category" => e => e.Category,
			_ => e => e.Id
		};

		return keySelector;
	}

	public async Task<Event> GetByIdAsync(Guid id, bool trackChanges) =>
		await GetByPredicate(e => e.Id.Equals(id), trackChanges)
		.SingleOrDefaultAsync();
}
