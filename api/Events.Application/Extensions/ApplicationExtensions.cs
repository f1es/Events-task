using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Events.Application.Validators;
using Events.Application.Services.ModelServices.Implementations;
using Events.Application.Services.ModelServices.Interfaces;
using Events.Application.MapperProfiles;
using FluentValidation.AspNetCore;
using Events.Application.Usecases.EventUsecases.Interfaces;
using Events.Application.Usecases.EventUsecases.Implementations;
using Events.Application.Usecases.ImageUsecases.Interfaces;
using Events.Application.Usecases.ImageUsecases.Implementations;
using Events.Application.Usecases.JwtProviderUsecases.Interfaces;
using Events.Application.Usecases.JwtProviderUsecases.Implementations;
using Events.Application.Usecases.ParticipantUsecases.Interfaces;
using Events.Application.Usecases.ParticipantUsecases.Implementations;
using Events.Application.Usecases.PasswordHasherUsecases.Interfaces;
using Events.Application.Usecases.PasswordHasherUsecases.Implementations;
using Events.Application.Usecases.RefreshProviderUsecase.Interfaces;
using Events.Application.Usecases.RefreshProviderUsecase.Implementations;
using Events.Application.Usecases.RefreshTokenUseCase.Implementations;
using Events.Application.Usecases.RefreshTokenUseCase.Interfaces;
using Events.Application.Usecases.UserUsecases.Interfaces;
using Events.Application.Usecases.UserUsecases.Implementations;

namespace Events.Application.Extensions;

public static class ApplicationExtensions
{
	public static void ConfigureValidators(this IServiceCollection services)
	{
		services.AddValidatorsFromAssemblyContaining<EventRequestDtoValidation>();
		services.AddFluentValidationAutoValidation();
	}

	public static void ConfigureServices(this IServiceCollection services) => 
		services.AddScoped<IServiceManager, ServiceManager>();

	public static void ConfigureUsecases(this IServiceCollection services)
	{
		services.AddScoped<IEventUseCaseManager, EventUseCaseManager>();
		services.AddScoped<IImageUseCaseManager, ImageUseCaseManager>();
		services.AddScoped<IJwtProviderUseCaseManager, JwtProviderUseCaseManager>();
		services.AddScoped<IParticipantUseCaseManager, ParticipantUseCaseManager>();
		services.AddScoped<IPasswordHasherUseCaseManager, PasswordHasherUseCaseManager>();
		services.AddScoped<IRefreshProviderUseCaseManager, RefreshProviderUseCaseManager>();
		services.AddScoped<IRefreshTokenUseCaseManager, RefreshTokenUseCaseManager>();
		services.AddScoped<IUserUseCaseManager, UserUseCaseManager>();
	}

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
