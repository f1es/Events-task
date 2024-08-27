using Events.Domain.Exceptions;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;

namespace Events.Application.Usecases.ImageUsecases.Extensions;

public static class RepositoryManagerExtensions
{
	public static async Task<Event> GetEventByIdAndCheckIfExistAsync(
		this IRepositoryManager repositoryManager,
		Guid eventId,
		bool trackChanges)
	{
		var eventModel = await repositoryManager.Event.GetByIdAsync(eventId, trackChanges);
		if (eventModel is null)
		{
			throw new NotFoundException($"event with id {eventId} not found");
		}

		return eventModel;
	}

	public static async Task<Image> GetImageByEventIdAndCheckIfExistAsync(
		this IRepositoryManager repositoryManager, 
		Guid eventId, 
		bool trackChanges)
	{
		var image = await repositoryManager.Image.GetImageAsync(eventId, trackChanges);
		if (image is null)
		{
			throw new NotFoundException($"image for event {eventId} not found");
		}

		return image;
	}
}
