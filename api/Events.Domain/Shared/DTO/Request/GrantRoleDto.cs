namespace Events.Domain.Shared.DTO.Request;

public record GrantRoleDto(
	Guid UserId,
	string Role);
