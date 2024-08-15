using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;

namespace Events.API.Extensions;

public static class HttpRequestExtensions
{
	public static string GetRefreshToken(this HttpRequest httpRequest)
	{
		httpRequest.Cookies.TryGetValue("ref", out string refreshToken);

		return refreshToken;
	}

	public static string GetAccessToken(this HttpRequest httpRequest)
	{
		httpRequest.Cookies.TryGetValue("acc", out string accessToken);

		return accessToken;
	}
}
