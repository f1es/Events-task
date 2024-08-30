using Events.Application.Usecases.JwtProviderUsecases.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Events.Application.Usecases.JwtProviderUsecases.Implementations;

public class GetUserIdUseCase : IGetUserIdUseCase
{
	public Guid GetUserId(string token)
	{
		var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
		var userIdString = jwt.Claims.First(c => c.Type == "userId").Value;
		var userId = Guid.Parse(userIdString);

		return userId;
	}
}
