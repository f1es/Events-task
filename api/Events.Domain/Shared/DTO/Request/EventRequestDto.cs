namespace Events.Domain.Shared.DTO.Request;

public record EventRequestDto(
	string Name,
	string Description,
	DateTime Date,
	string Place,
	string Category,
	int MaxParticipantsCount);
