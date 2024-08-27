using Events.Application.Options;
using Events.Application.Usecases.JwtProviderUsecases.Interfaces;
using Events.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Events.Application.Usecases.JwtProviderUsecases.Implementations;

public class GenerateTokenUseCase : IGenerateTokenUseCase
{
	private readonly JwtOptions _jwtOptions;
	public GenerateTokenUseCase(IOptions<JwtOptions> jwtOptions)
	{
		_jwtOptions = jwtOptions.Value;
	}
	public string GenerateToken(User user)
	{
		Claim[] claims = [
			new("userId", user.Id.ToString()),
			new("role", user.Role)
			];

		var signingCredentials = new SigningCredentials(
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)),
		SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			claims: claims,
			signingCredentials: signingCredentials,
			expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresHours));

		var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

		return tokenValue;
	}
}
