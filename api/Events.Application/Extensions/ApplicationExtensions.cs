using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Events.Application.Validators;
using Events.Application.Services.Implementations;
using Events.Application.Services.Interfaces;

namespace Events.Application.Extensions;

public static class ApplicationExtensions
{
	public static void ConfigureValidators(this IServiceCollection services)
	{
		services.AddValidatorsFromAssemblyContaining<EventForCreateRequestDtoValidation>();
	}

	public static void ConfigureServices(this IServiceCollection services) => 
		services.AddScoped<IServiceManager, ServiceManager>();
}
