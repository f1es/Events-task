namespace Events.Domain.Shared.DTO.Response;

public record ImageResponseDto(
	byte[] Content,
	string Type);
