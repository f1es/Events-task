namespace Events.Domain.Shared.DTO.Response;

public record EventResponseDto(
	Guid Id,
	string Name,
	string Description,
	DateTime Date,
	string Place,
	string Category,
	int MaxParticipantsCount
	);
