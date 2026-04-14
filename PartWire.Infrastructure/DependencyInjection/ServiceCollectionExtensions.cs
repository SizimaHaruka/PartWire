using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PartWire.Application.Abstractions.Repositories;
using PartWire.Infrastructure.Configuration;
using PartWire.Infrastructure.Persistence;
using PartWire.Infrastructure.Repositories;

namespace PartWire.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var databaseOptions = configuration
            .GetSection(DatabaseOptions.SectionName)
            .Get<DatabaseOptions>() ?? new DatabaseOptions();

        services.AddSingleton(databaseOptions);

        services.AddDbContext<PartWireDbContext>(options =>
        {
            if (string.Equals(databaseOptions.Provider, "SqlServer", StringComparison.OrdinalIgnoreCase))
            {
                options.UseSqlServer(databaseOptions.ConnectionString);
            }
            else
            {
                options.UseSqlite(databaseOptions.ConnectionString);
            }
        });

        if (databaseOptions.UseStubData)
        {
            services.AddSingleton<IProjectRepository, StubProjectRepository>();
        }
        else
        {
            services.AddScoped<IProjectRepository, EfProjectRepository>();
        }

        return services;
    }
}
