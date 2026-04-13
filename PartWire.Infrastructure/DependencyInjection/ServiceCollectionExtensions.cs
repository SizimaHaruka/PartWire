using Microsoft.Extensions.DependencyInjection;
using PartWire.Application.Abstractions.Repositories;
using PartWire.Infrastructure.Repositories;

namespace PartWire.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IProjectRepository, StubProjectRepository>();

        return services;
    }
}
