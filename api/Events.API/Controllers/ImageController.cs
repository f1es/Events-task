using Events.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Events.API.Controllers;

[ApiController]
[Route("api/events/{eventId:guid}/image")]
public class ImageController : ControllerBase
{
	private readonly IServiceManager _serviceManager;
    public ImageController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpPost]
	public async Task<IActionResult> UploadImage(Guid eventId,  IFormFile image)
	{
		await _serviceManager.ImageService.UploadImageAsync(eventId, image, trackChanges: false);

		return NoContent();
	}

	[HttpGet]
	public async Task<IActionResult> GetImage(Guid eventId)
	{
		var image = await _serviceManager.ImageService.GetImageAsync(eventId, trackChanges: false);

		return File(image.Content, image.Type);
	}
	[HttpPut]
	public async Task<IActionResult> UpdateImage(Guid eventId, IFormFile image)
	{
		await _serviceManager.ImageService.UpdateImageAsync(eventId, image, trackChanges: true);

		return NoContent();
	}
}
