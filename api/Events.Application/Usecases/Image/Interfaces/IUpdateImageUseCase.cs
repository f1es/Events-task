using Microsoft.AspNetCore.Http;

namespace Events.Application.Usecases.Image.Interfaces;

public interface IUpdateImageUseCase
{
	Task UpdateImageAsync(Guid eventId, IFormFile imageForm, bool trackChanges);
}
