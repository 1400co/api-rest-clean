using Microsoft.AspNetCore.Mvc;

namespace ApiRestClean.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "OK",
            environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
            time = DateTime.UtcNow
        });
    }
}
