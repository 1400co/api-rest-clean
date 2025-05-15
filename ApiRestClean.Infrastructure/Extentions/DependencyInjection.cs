using Microsoft.Extensions;
using Microsoft.Extensions.DependencyInjection;
using ApiRestClean.Core.Interfaces;
using ApiRestClean.Infrastructure.Repositories;
using MediatR;
using FluentValidation;
using ApiRestClean.Core.Features.PipelineBehaviors;
using System.Reflection;

namespace ApiRestClean.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IProductRepository, InMemoryProductRepository>();
        services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.Load("ApiRestClean.Core"));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>), ServiceLifetime.Scoped);
            });

        // Registra todos los validadores de FluentValidation en el assembly de Core
        services.AddValidatorsFromAssembly(Assembly.Load("ApiRestClean.Core"));

        // Si usaras EF Core: services.AddDbContext<AppDbContext>(options => ...);

        return services;
    }
}
