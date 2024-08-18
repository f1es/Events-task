using Events.Domain.Models;
using Events.Domain.Shared.DTO.Response;
using Microsoft.AspNetCore.Http;

namespace Events.Application.Services.ModelServices.Interfaces;

public interface IImageService
{
    Task<ImageResponseDto> UploadImageAsync(Guid eventId, IFormFile image, bool trackChanges);
    Task<ImageResponseDto> GetImageAsync(Guid eventId, bool trackChanges);
    Task UpdateImageAsync(Guid eventId, IFormFile imageForm, bool trackChanges);
	Task DeleteImageAsync(Guid eventId, bool trackChanges);
}
