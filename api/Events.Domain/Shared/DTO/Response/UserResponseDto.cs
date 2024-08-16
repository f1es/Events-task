namespace Events.Domain.Shared.DTO.Response;

public record UserResponseDto(
	Guid Id,
	string Username,
	string Email,
	string Role);
