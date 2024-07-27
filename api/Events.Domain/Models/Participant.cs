namespace Events.Domain.Models;

public class Participant
{
	public Guid Id { get; set; }
	public string? Name { get; set; }
	public string? Surname { get; set; }
	public DateTime Birtday { get; set; }
	public DateTime RegistrationDate { get; set; }
	public string? Email { get; set; }
	public Guid? EventId { get; set; }
	public Event? Event { get; set; }
}
