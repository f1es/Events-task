using Events.Application.Options;
using Events.Application.Services.SecurityServices.Interfaces;
using Events.Domain.Models;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Events.Application.Services.SecurityServices.Implementations;

public class RefreshProvider : IRefreshProvider
{
    private readonly RefreshTokenOptions _refreshTokenOptions;
    public RefreshProvider(IOptions<RefreshTokenOptions> refreshTokenOptions)
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
