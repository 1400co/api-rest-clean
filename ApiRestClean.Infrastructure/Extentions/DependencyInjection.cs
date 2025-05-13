using Microsoft.Extensions;
using Microsoft.Extensions.DependencyInjection;
using ApiRestClean.Core.Interfaces;
using ApiRestClean.Infrastructure.Repositories;

namespace ApiRestClean.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Aquí registramos todas las implementaciones concretas
        services.AddSingleton<IProductRepository, InMemoryProductRepository>();

        // Si usaras EF Core: services.AddDbContext<AppDbContext>(options => ...);

        return services;
    }
}
