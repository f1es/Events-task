using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Events.Application.Validators;
using Events.Application.Services.ModelServices.Implementations;
using Events.Application.Services.ModelServices.Interfaces;
using Events.Application.MapperProfiles;
using FluentValidation.AspNetCore;

namespace Events.Application.Extensions;

public static class ApplicationExtensions
{
	public static void ConfigureValidators(this IServiceCollection services)
	{
		services.AddValidatorsFromAssemblyContaining<EventForCreateRequestDtoValidation>();
		services.AddFluentValidationAutoValidation();
	}

	public static void ConfigureServices(this IServiceCollection services) => 
		services.AddScoped<IServiceManager, ServiceManager>();

	public static void ConfigureMapperProfiles(this IServiceCollection services)
	{
		services.AddAutoMapper(options =>
		{
			options.AddProfile<EventMapperProfile>();
			options.AddProfile<RefreshTokenMapperProfile>();
			options.AddProfile<UserMapperProfile>();
			options.AddProfile<ImageMapperProfile>();
			options.AddProfile<ParticipantMapperProfile>();
		});
	}
}
