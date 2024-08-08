using Events.Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Events.API.Extensions;

public static class ServicesExtension
{
    public static void ConfigureApiAuthentication(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
				};

				options.Events = new JwtBearerEvents()
				{
					OnMessageReceived = context =>
					{
						context.Token = context.Request.Cookies["cook"];

						return Task.CompletedTask;
					}
				};
			});

		services.AddAuthorization();
	}

	public static void ConfigureCors(IServiceCollection services)
	{
		services.AddCors(options =>
		{
			options.AddPolicy("CorsPolicy", builder =>
			builder.AllowAnyOrigin()
			.AllowAnyHeader()
			.AllowAnyMethod());
		});
	}
}
