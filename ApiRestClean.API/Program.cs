using ApiRestClean.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configuración del pipeline HTTP
ConfigureMiddleware(app, app.Environment);

// Configuración de endpoints
ConfigureEndpoints(app);

app.Run();

// Métodos de configuración
void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddControllers();
    
    // Aquí puedes agregar más configuraciones de servicios
}

void ConfigureMiddleware(WebApplication app, IHostEnvironment env)
{
    // Middleware de desarrollo
    if (env.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    app.UseHttpsRedirection();
    
    // Aquí puedes agregar más middleware
}

void ConfigureEndpoints(WebApplication app)
{
    app.MapControllers();          // <- NECESARIO PARA CONTROLLERS
    app.RegisterAllEndpoints();    // <- Minimal APIs automáticas
}

