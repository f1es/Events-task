using Events.Domain.Shared.DTO.Response;

namespace Events.Application.Usecases.Image.Interfaces;

public interface IGetImageUseCase
{
	Task<ImageResponseDto> GetImageAsync(Guid eventId, bool trackChanges);
}
