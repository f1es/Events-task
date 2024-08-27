using Events.Domain.Exceptions;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;

namespace Events.Application.Usecases.ParticipantUsecases.Extensions;

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
	public static async Task<Participant> GetParticipantByIdAndCheckIfExistAsync(
		this IRepositoryManager repositoryManager, 
		Guid eventId, 
		Guid id,
		bool trackChanges)
	{
		var participantModel = await repositoryManager.Participant.GetByIdAsync(id, trackChanges);
		if (participantModel is null)
		{
			throw new NotFoundException($"participant with id {id} not found");
		}

		return participantModel;
	}
	public static async Task<User> GetUserByIdAndCheckIfExistAsync(
		this IRepositoryManager repositoryManager,
		Guid userId,
		bool trackChanges)
	{
		var userModel = await repositoryManager.User.GetByIdAsync(userId, trackChanges);
		if (userModel is null)
		{
			throw new NotFoundException($"user with id {userId} not found");
		}

		return userModel;
	}
}
