namespace Events.Domain.Shared.DTO.Response;

public record ParticipantResponseDto(
	string Name,
	string Surname,
	DateTime Birthday,
	DateTime RegistrationDate,
	string Email);
