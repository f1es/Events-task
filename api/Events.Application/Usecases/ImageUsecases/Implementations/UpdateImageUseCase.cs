using AutoMapper;
using Events.Application.Usecases.ImageUsecases.Extensions;
using Events.Application.Usecases.ImageUsecases.Interfaces;
using Events.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Events.Application.Usecases.ImageUsecases.Implementations;

public class UpdateImageUseCase : IUpdateImageUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IMapper _mapper;

	public UpdateImageUseCase(IRepositoryManager repositoryManager, IMapper mapper)
	{
		_repositoryManager = repositoryManager;
		_mapper = mapper;
	}

	public async Task UpdateImageAsync(Guid eventId, IFormFile imageForm, bool trackChanges)
	{
		await _repositoryManager.GetEventByIdAndCheckIfExistAsync(eventId, trackChanges);

		var image = await _repositoryManager.GetImageByEventIdAndCheckIfExistAsync(eventId, trackChanges);

		image = _mapper.Map(imageForm, image);

		await _repositoryManager.SaveAsync();
	}
}
