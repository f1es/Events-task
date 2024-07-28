using System.Linq.Expressions;

namespace Events.Application.Repositories.Interfaces;

public interface IRepositoryBase<T>
{
	IQueryable<T> GetAll(bool trackChanges);
	IQueryable<T> GetByPredicate(Expression<Func<T, bool>> predicate, bool trackChanges);
	void Create(T entity);
	void Update(T entity);
	void Delete(T entity);
}
