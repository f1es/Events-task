namespace Events.Domain.Models;

public class Event
{
	public Guid Id { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; }
	public DateTime Date { get; set; }
	public string? Place { get; set; }
	public string? Category { get; set; }
	public int MaxParticipantCount { get; set; }
	public string? Picture { get; set; }
	public ICollection<Participant>? Participants { get; set; }
}
