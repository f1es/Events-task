namespace Events.Domain.Shared.DTO.Request;

public record ParticipantRequestDto(
	string Name,
	string Surname,
	DateTime Birthday,
	DateTime RegistrationDate,
	string Email);
