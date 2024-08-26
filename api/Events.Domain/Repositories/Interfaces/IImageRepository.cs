using Events.Domain.Models;

namespace Events.Domain.Repositories.Interfaces;

public interface IImageRepository : IBaseRepository<Image>
{
	Task<Image> GetImageAsync(Guid eventId, bool trackChanges);
}
