﻿namespace Events.Domain.Models;

public class User
{
	public Guid Id { get; set; }
	public string? Username { get; set; }
	public string? Email { get; set; }
	public string? PasswordHash { get; set; }
	public RefreshToken RefreshToken { get; set; }
	public string? Role { get; set; }

	public ICollection<Participant>? Participants { get; set; }
}
