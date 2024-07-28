namespace Events.Domain.Shared.DTO.Request;

public record ParticipantForUpdateRequestDto(
	string Name,
	string Surname,
	DateTime Birthday,
	DateTime RegistrationDate,
	string Email);
