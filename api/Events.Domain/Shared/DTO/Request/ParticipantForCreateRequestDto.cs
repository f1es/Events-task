namespace Events.Domain.Shared.DTO.Request;

public record ParticipantForCreateRequestDto(
	string Name,
	string Surname,
	DateTime Birthday,
	DateTime RegistrationDate,
	string Email);
