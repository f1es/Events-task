namespace Events.Domain.Shared.DTO.Request;

public record UserLoginRequestDto(
	string Username,
	string Password);
