using System.Reflection;
using ApiRestClean.API.Endpoints;

namespace ApiRestClean.API.Extensions;

public static class EndpointRegistration
{
    /// <summary>
    /// Registra automáticamente todos los endpoints de la aplicación que implementan la interfaz IEndpoint.
    /// </summary>
    /// <param name="app">La instancia de WebApplication donde se registrarán los endpoints.</param>
    public static void RegisterAllEndpoints(this WebApplication app)
    {
        // Busca todos los tipos en el ensamblado actual que implementan IEndpoint
        // excluyendo interfaces y clases abstractas
        var endpointTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IEndpoint).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        // Crea una instancia de cada tipo de endpoint encontrado y registra sus rutas
        foreach (var type in endpointTypes)
        {
            var endpoint = (IEndpoint)Activator.CreateInstance(type)!;
            endpoint.RegisterRoutes(app);
        }
    }
}
