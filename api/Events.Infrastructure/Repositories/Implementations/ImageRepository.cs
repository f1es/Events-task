using Events.Application.Repositories.Interfaces;
using Events.Domain.Models;
using Events.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Events.Infrastructure.Repositories.Implementations;

public class ImageRepository : BaseRepository<Image>, IImageRepository
{
	private readonly EventsDBContext _eventsDBContext;
    public ImageRepository(EventsDBContext eventsDBContext)
		:base(eventsDBContext)
    {
        _eventsDBContext = eventsDBContext;
    }

	public void CreateImage(Guid eventId, Image image)
	{
		image.EventId = eventId;
		Create(image);
	}

	public Task<Image> GetImageAsync(Guid eventId, bool trackChanges) =>
		GetByPredicate(i => i.EventId.Equals(eventId), trackChanges)
		.SingleOrDefaultAsync();
}
