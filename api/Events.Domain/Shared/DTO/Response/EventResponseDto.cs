namespace Events.Domain.Shared.DTO.Response;

public record EventResponseDto(
	string Name,
	string Description,
	DateTime Date,
	string Place,
	string Category,
	int MaxParticipants
	);
