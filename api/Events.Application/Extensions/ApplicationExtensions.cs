using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Events.Application.Validators;

namespace Events.Application.Extensions;

public static class ApplicationExtensions
{
	public static IServiceCollection ConfigureValidators(this IServiceCollection services)
	{
		return services.AddValidatorsFromAssemblyContaining<EventForCreateRequestDtoValidation>();
	}
}
