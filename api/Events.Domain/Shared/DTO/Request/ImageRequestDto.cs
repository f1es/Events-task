using Microsoft.AspNetCore.Http;

namespace Events.Domain.Shared.DTO.Request;

public class ImageRequestDto
{
	public IFormFile Image { get; set; }
}
