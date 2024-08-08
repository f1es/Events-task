using Events.Application.JWT.Interfaces;
using Events.Domain.Models;
using System.Security.Cryptography;

namespace Events.Application.JWT.Implementations;

public class RefreshProvider : IRefreshProvider
{
	public RefreshToken GenerateToken(Guid userId)
	{
		var refreshToken = new RefreshToken()
		{
			Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
			Created = DateTime.UtcNow,
			Expires = DateTime.UtcNow.AddDays(14),
			UserId = userId
		};

		return refreshToken;
	}
}
