using AutoMapper;
using Events.Application.Usecases.ImageUsecases.Extensions;
using Events.Application.Usecases.ImageUsecases.Interfaces;
using Events.Domain.Models;
using Events.Domain.Repositories.Interfaces;
using Events.Domain.Shared.DTO.Response;
using Microsoft.AspNetCore.Http;

namespace Events.Application.Usecases.ImageUsecases.Implementations;

public class UploadImageUseCase : IUploadImageUseCase
{
	private readonly IRepositoryManager _repositoryManager;
	private readonly IMapper _mapper;

	public UploadImageUseCase(IRepositoryManager repositoryManager, IMapper mapper)
	{
		_repositoryManager = repositoryManager;
		_mapper = mapper;
	}

	public async Task<ImageResponseDto> UploadImageAsync(Guid eventId, IFormFile imageForm, bool trackChanges)
	{
		await _repositoryManager.GetEventByIdAndCheckIfExistAsync(eventId, trackChanges);

		var image = _mapper.Map<Image>(imageForm);
		image.EventId = eventId;

		_repositoryManager.Image.Create(image);

		await _repositoryManager.SaveAsync();

		var imageResponse = _mapper.Map<ImageResponseDto>(image);

		return imageResponse;
	}
}
