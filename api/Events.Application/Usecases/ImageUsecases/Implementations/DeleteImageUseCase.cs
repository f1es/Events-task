using AutoMapper;
using Events.Application.Usecases.ImageUsecases.Extensions;
using Events.Application.Usecases.ImageUsecases.Interfaces;
using Events.Domain.Exceptions;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;

namespace Events.Application.Usecases.ImageUsecases.Implementations;

public class DeleteImageUseCase : IDeleteImageUseCase
{
	private readonly IRepositoryManager _repositoryManager;

	public DeleteImageUseCase(
		IRepositoryManager repositoryManager)
	{
		_repositoryManager = repositoryManager;
	}

	public async Task DeleteImageAsync(Guid eventId, bool trackChanges)
	{
		await _repositoryManager.GetEventByIdAndCheckIfExistAsync(eventId, trackChanges);

		var image = await _repositoryManager.GetImageByEventIdAndCheckIfExistAsync(eventId, trackChanges);

		_repositoryManager.Image.Delete(image);

		await _repositoryManager.SaveAsync();
	}
}
