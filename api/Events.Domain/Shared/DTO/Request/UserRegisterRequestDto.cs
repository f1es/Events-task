namespace Events.Domain.Shared.DTO.Request;

public record UserRegisterRequestDto(
	string Username,
	string Email,
	string Password);
