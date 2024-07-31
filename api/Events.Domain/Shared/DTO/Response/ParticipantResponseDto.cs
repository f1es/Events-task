namespace Events.Domain.Shared.DTO.Response;

public record ParticipantResponseDto(
	Guid Id,
	string Name,
	string Surname,
	DateTime Birthday,
	DateTime RegistrationDate,
	string Email);
