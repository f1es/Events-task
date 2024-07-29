using Events.Domain.Models;

namespace Events.Application.Repositories.Interfaces;

public interface IImageRepository : IRepositoryBase<Image>
{
	Task<Image> GetImageAsync(Guid eventId, bool trackChanges);
	void CreateImage(Guid eventId, Image image);
}
