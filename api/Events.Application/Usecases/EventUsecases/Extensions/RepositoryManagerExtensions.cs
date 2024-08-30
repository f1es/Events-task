using Events.Domain.Exceptions;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;

namespace Events.Application.Usecases.EventUsecases.Extensions;

public static class RepositoryManagerExtensions
{
	public static async Task<Event> GetEventByIdAndCheckIfExistAsync(
		this IRepositoryManager repositoryManager, 
		Guid id, 
		bool trackChanges)
	{
		var eventModel = await repositoryManager.Event.GetByIdAsync(id, trackChanges);

		if (eventModel is null)
		{
			throw new NotFoundException($"event with id {id} not found");
		}

		return eventModel;
	}
}
