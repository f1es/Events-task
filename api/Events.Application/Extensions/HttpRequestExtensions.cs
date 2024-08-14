using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;

namespace Events.Application.Extensions;

public static class HttpRequestExtensions
{
	public static string GetRefreshToken(this HttpRequest httpRequest)
	{
		httpRequest.Cookies.TryGetValue("ref", out string refreshToken);

		return refreshToken;
	}
}
