using AutoMapper;
using Events.Application.Extensions;
using Events.Domain.Repositories.Interfaces;
using Events.Application.Services.ModelServices.Interfaces;
using Events.Domain.Exceptions;
using Events.Domain.Models;
using Events.Domain.Shared.DTO.Response;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Events.Application.Services.ModelServices.Implementations;

public class ImageService : IImageService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public ImageService(
        IRepositoryManager repositoryManager,
        IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<ImageResponseDto> UploadImageAsync(Guid eventId, IFormFile imageForm, bool trackChanges)
    {
        await GetEventByIdAndCheckIfExistAsync(eventId, trackChanges);

        var image = _mapper.Map<Image>(imageForm);
        image.EventId = eventId;

        _repositoryManager.Image.Create(image);

        await _repositoryManager.SaveAsync();

        var imageResponse = _mapper.Map<ImageResponseDto>(image);

        return imageResponse;
    }

    public async Task<ImageResponseDto> GetImageAsync(Guid eventId, bool trackChanges)
    {
        await GetEventByIdAndCheckIfExistAsync(eventId, trackChanges);

        var image = await GetImageByEventIdAndCheckIfExistAsync(eventId, trackChanges);

        var imageResponse = _mapper.Map<ImageResponseDto>(image);

        return imageResponse;
    }

    public async Task UpdateImageAsync(Guid eventId, IFormFile imageForm, bool trackChanges)
    {
        await GetEventByIdAndCheckIfExistAsync(eventId, trackChanges);

        var image = await GetImageByEventIdAndCheckIfExistAsync(eventId, trackChanges);

		image = _mapper.Map(imageForm, image);

        await _repositoryManager.SaveAsync();
    }

    public async Task DeleteImageAsync(Guid eventId, bool trackChanges)
    {
        await GetEventByIdAndCheckIfExistAsync(eventId, trackChanges);

        var image = await GetImageByEventIdAndCheckIfExistAsync(eventId, trackChanges);

		_repositoryManager.Image.Delete(image);

		await _repositoryManager.SaveAsync();
	}

    private async Task<Event> GetEventByIdAndCheckIfExistAsync(Guid eventId, bool trackChanges)
    {
        var eventModel = await _repositoryManager.Event.GetByIdAsync(eventId, trackChanges);
        if (eventModel is null)
        {
            throw new NotFoundException($"event with id {eventId} not found");
        }

        return eventModel;
    }

    private async Task<Image> GetImageByEventIdAndCheckIfExistAsync(Guid eventId, bool trackChanges)
    {
		var image = await _repositoryManager.Image.GetImageAsync(eventId, trackChanges);
		if (image is null)
		{
			throw new NotFoundException($"image for event {eventId} not found");
		}

        return image;
	}
}