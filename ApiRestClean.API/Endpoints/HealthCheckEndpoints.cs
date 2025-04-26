namespace ApiRestClean.API.Endpoints;

public class HealthCheckEndpoint : IEndpoint
{
    public void RegisterRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/healthcheck", () =>
        {
            return Results.Ok(new
            {
                status = "Healthy ✅",
                environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                time = DateTime.UtcNow
            });
        })
        .WithName("HealthCheck")
        .WithTags("System")
        .WithOpenApi();
    }
}
