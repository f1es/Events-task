namespace Events.Domain.Shared.DTO.Request;

public record EventForUpdateRequestDto(
	string Name,
	string Description,
	DateTime Date,
	string Place,
	string Category,
	int MaxParticipantCount);
