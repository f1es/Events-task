using Events.Domain.Shared.DTO.Response;
using Microsoft.AspNetCore.Http;

namespace Events.Application.Usecases.ImageUsecases.Interfaces;

public interface IUploadImageUseCase
{
    Task<ImageResponseDto> UploadImageAsync(Guid eventId, IFormFile imageForm, bool trackChanges);
}
