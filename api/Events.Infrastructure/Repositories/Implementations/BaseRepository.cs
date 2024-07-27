using Events.Application.Repositories.Interfaces;
using Events.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Events.Infrastructure.Repositories.Implementations;

public abstract class BaseRepository<T> : IRepositoryBase<T> where T : class
{
	protected readonly EventsDBContext eventsDBContext;
    public BaseRepository(EventsDBContext eventsDBContext)
    {
        this.eventsDBContext = eventsDBContext;
    }
	public void Create(T entity) =>
		eventsDBContext
		.Set<T>()
		.Add(entity);

	public void Delete(T entity) =>
		eventsDBContext
		.Set<T>()
		.Remove(entity);

	public void Update(T entity) =>
		eventsDBContext 
		.Set<T>()
		.Update(entity);

	public IQueryable<T> GetAll() =>
		eventsDBContext
		.Set<T>()
		.AsNoTracking();

	public IQueryable<T> GetByPredicate(Expression<Func<T, bool>> predicate) =>
		eventsDBContext 
		.Set<T>()
		.Where(predicate)
		.AsNoTracking();
}
