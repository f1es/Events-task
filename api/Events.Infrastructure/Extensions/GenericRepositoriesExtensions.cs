using Events.Domain.Exceptions;

namespace Events.Infrastructure.Extensions;

public static class GenericRepositoriesExtensions
{
	public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int page, int pageSize)
	{
		if (page <= 0 || pageSize <= 0)
		{
			throw new BadRequestException("Page or page size must be larger than zero");
		}

		query = query
			.Skip((page - 1) * pageSize)
			.Take(pageSize);

		return query;
	}
}
