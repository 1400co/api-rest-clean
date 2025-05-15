namespace ApiRestClean.API.Endpoints;

using ApiRestClean.Core.Features.Products;
using MediatR;

public class CreateProductEndpoint : IEndpoint
{
    public void RegisterRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/vertical-slice-products", async (CreateProduct.Request command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.Created($"/products/{result.Value.Id}", result.Value);
        })
        .WithName("CreateProduct")
        .WithTags("Products-Vertical-Slice");
    }
}