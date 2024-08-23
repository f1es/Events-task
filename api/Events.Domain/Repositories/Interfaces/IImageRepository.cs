using Events.Domain.Models;

namespace Events.Domain.Repositories.Interfaces;

public interface IImageRepository
{
	Task<Image> GetImageAsync(Guid eventId, bool trackChanges);
	void CreateImage(Guid eventId, Image image);
	void DeleteImage(Image image);
	void UpdateImage(Guid eventId, Image image);
}
