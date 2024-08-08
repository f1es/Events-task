using Events.Domain.Models;
using Microsoft.AspNetCore.Http;

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

	public static void AppendAccessToken(this HttpResponse response, string accessToken) =>
		response.Cookies.Append("cook", accessToken);
}
