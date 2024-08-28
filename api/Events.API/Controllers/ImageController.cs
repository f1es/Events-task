using Events.API.Attributes;
using Events.Application.Services.ModelServices.Interfaces;
using Events.Application.Usecases.ImageUsecases.Interfaces;
using Events.Domain.Shared.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events.API.Controllers;

[ApiController]
[Route("api/events/{eventId:guid}/image")]
[Authorize]
public class ImageController : ControllerBase
{
	private readonly IImageUseCaseManager _imageUseCaseManager;
    public ImageController(IImageUseCaseManager imageUseCaseManager)
    {
		_imageUseCaseManager = imageUseCaseManager;
    }
    [HttpPost]
	[RequiredRole("Admin, Manager")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> UploadImage(Guid eventId, [FromForm] ImageRequestDto image)
	{
		await _imageUseCaseManager.UpdateImageUseCase.UpdateImageAsync(eventId, image.Image, trackChanges: false);

		return NoContent();
	}

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetImage(Guid eventId)
	{
		var image = await _imageUseCaseManager.GetImageUseCase.GetImageAsync(eventId, trackChanges: false);

		return File(image.Content, image.Type);
	}
	[HttpPut]
	[RequiredRole("Admin, Manager")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> UpdateImage(Guid eventId, [FromForm] ImageRequestDto image)
	{
		await _imageUseCaseManager.UpdateImageUseCase.UpdateImageAsync(eventId, image.Image, trackChanges: true);

		return NoContent();
	}
	[HttpDelete]
	[RequiredRole("Admin, Manager")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> DeleteImage(Guid eventId)
	{
		await _imageUseCaseManager.DeleteImageUseCase.DeleteImageAsync(eventId, trackChanges: false);

		return NoContent();
	}
}
