using System.Reflection;
using ApiRestClean.API.Endpoints;

namespace ApiRestClean.API.Extensions;

public static class EndpointRegistration
{
    public static void RegisterAllEndpoints(this WebApplication app)
    {
        var endpointTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IEndpoint).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var type in endpointTypes)
        {
            var endpoint = (IEndpoint)Activator.CreateInstance(type)!;
            endpoint.RegisterRoutes(app);
        }
    }
}
