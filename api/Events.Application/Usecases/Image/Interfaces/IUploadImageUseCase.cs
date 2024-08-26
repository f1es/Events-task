using Events.Domain.Shared.DTO.Response;
using Microsoft.AspNetCore.Http;

namespace Events.Application.Usecases.Image.Interfaces;

public interface IUploadImageUseCase
{
	Task<ImageResponseDto> UploadImageAsync(Guid eventId, IFormFile image, bool trackChanges);
}
