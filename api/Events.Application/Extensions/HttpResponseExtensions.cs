using Events.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Events.Application.Extensions;

public static class HttpResponseExtensions
{
	public static void AppendRefreshToken(this HttpResponse response, RefreshToken refreshToken)
	{
		var cookieOptions = new CookieOptions
		{
			HttpOnly = true,
			Expires = refreshToken.Expires,
		};

		response.Cookies.Append("ref", refreshToken.Token);
	}

	public static void AppendAccessToken(this HttpResponse response, string accessToken)
	{
		var expiresDate = GetExpirationDate(accessToken);

		var cookieOptions = new CookieOptions
		{
			HttpOnly = true,
			Expires = expiresDate
		};

		response.Cookies.Append("acc", accessToken, cookieOptions);
	}

	private static DateTime GetExpirationDate(string jwtToken)
	{
		var jwt = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);

		return jwt.ValidTo;
	}
		
}
