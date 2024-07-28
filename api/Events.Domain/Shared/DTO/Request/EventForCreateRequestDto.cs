namespace Events.Domain.Shared.DTO.Request;

public record EventForCreateRequestDto(
	string Name, 
	string Description, 
	DateTime Date, 
	string Place, 
	string Category,
	int MaxParticipantCount);
