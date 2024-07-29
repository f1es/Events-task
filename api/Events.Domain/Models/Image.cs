namespace Events.Domain.Models;

public class Image
{
	public Guid Id { get; set; }
	public string? Name { get; set; }
	public string? Type { get; set; }
	public byte[]? Content { get; set; }

	public Guid EventId { get; set; }
	public Event Event { get; set; }
}
