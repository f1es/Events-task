using Events.Application.Repositories.Interfaces;
using Events.Infrastructure.Repositories.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Events.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
	public static void ConfigureRepositories(this IServiceCollection services) =>
		services.AddScoped<IRepositoryManager, RepositoryManager>();
}
