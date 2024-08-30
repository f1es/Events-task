using Events.Application.Options;
using Events.Application.Usecases.RefreshProviderUsecase.Interfaces;
using Events.Domain.Models;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Events.Application.Usecases.RefreshProviderUsecase.Implementations;

public class GenerateRefreshTokenUseCase : IGenerateRefreshTokenUseCase
{
	private readonly RefreshTokenOptions _refreshTokenOptions;
	public GenerateRefreshTokenUseCase(IOptions<RefreshTokenOptions> refreshTokenOptions)
	{
		_refreshTokenOptions = refreshTokenOptions.Value;
	}
	public RefreshToken GenerateToken(Guid userId)
	{
		var refreshToken = new RefreshToken()
		{
			Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
			Created = DateTime.UtcNow,
			Expires = DateTime.UtcNow.AddDays(_refreshTokenOptions.ExpiresDays),
			UserId = userId
		};

		return refreshToken;
	}
}
