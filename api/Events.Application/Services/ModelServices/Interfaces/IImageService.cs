using Events.Domain.Models;
using Events.Domain.Shared.DTO.Response;
using Microsoft.AspNetCore.Http;

namespace Events.Application.Services.ModelServices.Interfaces;

public interface IImageService
{
    public Task<ImageResponseDto> UploadImageAsync(Guid eventId, IFormFile image, bool trackChanges);
    public Task<ImageResponseDto> GetImageAsync(Guid eventId, bool trackChanges);
    public Task UpdateImageAsync(Guid eventId, IFormFile imageForm, bool trackChanges);
}
