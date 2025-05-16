using ApiRestClean.Core.Features.Products;
using MediatR;

namespace ApiRestClean.API.Endpoints;

public class UpdateProductEndpoint : IEndpoint
{
    public void RegisterRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/vertical-slice-products/{id:guid}", async (Guid id, UpdateProduct.UpdateProductRequest command, ISender sender) =>
        {
            // Aseguramos que el id de la ruta y el del body sean iguales
            if (id != command.Id)
                return Results.BadRequest("El id de la ruta y el del body deben ser iguales.");

            var result = await sender.Send(command);

            if (result.IsFailed)
                return Results.NotFound(result.Errors.First().Message);

            return Results.Ok(result.Value);
        })
        .WithName("UpdateProduct")
        .WithTags("Products-Vertical-Slice");
    }
} 