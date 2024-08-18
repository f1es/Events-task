using Events.API.Attributes;
using Events.Application.Services.ModelServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events.API.Controllers;

[ApiController]
[Route("api/events/{eventId:guid}/image")]
[Authorize]
public class ImageController : ControllerBase
{
	private readonly IServiceManager _serviceManager;
    public ImageController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpPost]
	[RequiredRole("Admin, Manager")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> UploadImage(Guid eventId,  IFormFile image)
	{
		await _serviceManager.ImageService.UploadImageAsync(eventId, image, trackChanges: false);

		return NoContent();
	}

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetImage(Guid eventId)
	{
		var image = await _serviceManager.ImageService.GetImageAsync(eventId, trackChanges: false);

		return File(image.Content, image.Type);
	}
	[HttpPut]
	[RequiredRole("Admin, Manager")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> UpdateImage(Guid eventId, IFormFile image)
	{
		await _serviceManager.ImageService.UpdateImageAsync(eventId, image, trackChanges: true);

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
		await _serviceManager.ImageService.DeleteImageAsync(eventId, trackChanges: false);

		return NoContent();
	}
}
