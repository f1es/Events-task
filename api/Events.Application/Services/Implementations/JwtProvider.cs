using Events.Application.Services.Interfaces;
using Events.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Events.Application.Services.Implementations;

public class JwtProvider : IJwtProvider
{
	private readonly JwtOptions _jwtOptions;
    public JwtProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }
    public string GenerateToken(User user)
	{
		Claim[] claims = [new("UserId", user.Id.ToString())];

		var signingCredentials = new SigningCredentials(
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)),
			SecurityAlgorithms.HmacSha256);

		var tocken = new JwtSecurityToken(
			claims: claims,
			signingCredentials:  signingCredentials,
			expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresHours));

		var tockenValue = new JwtSecurityTokenHandler().WriteToken(tocken);

		return tockenValue;
	}
}