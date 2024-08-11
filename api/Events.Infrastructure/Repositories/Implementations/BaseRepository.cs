using Events.Application.Repositories.Interfaces;
using Events.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Events.Infrastructure.Repositories.Implementations;

public abstract class BaseRepository<T> where T : class
{
	protected readonly EventsDBContext eventsDBContext;
    public BaseRepository(EventsDBContext eventsDBContext)
    {
        this.eventsDBContext = eventsDBContext;
    }
	protected void Create(T entity) =>
		eventsDBContext
		.Set<T>()
		.Add(entity);

	protected void Delete(T entity) =>
		eventsDBContext
		.Set<T>()
		.Remove(entity);

	protected void Update(T entity) =>
		eventsDBContext 
		.Set<T>()
		.Update(entity);

	protected IQueryable<T> GetAll(bool trackChanges) =>
		trackChanges ?
		eventsDBContext
		.Set<T>() 
		:
		eventsDBContext
		.Set<T>()
		.AsNoTracking();

	protected IQueryable<T> GetByPredicate(Expression<Func<T, bool>> predicate, bool trackChanges) =>
		trackChanges ?
		eventsDBContext 
		.Set<T>()
		.Where(predicate) 
		:
		eventsDBContext
		.Set<T>()
		.Where(predicate)
		.AsNoTracking();
}
