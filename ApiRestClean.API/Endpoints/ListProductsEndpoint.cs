using ApiRestClean.Core.Features.Products;
using MediatR;

namespace ApiRestClean.API.Endpoints;

public class ListProductsEndpoint : IEndpoint
{
    public void RegisterRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/vertical-slice-list-products", async (string? name, ISender sender) =>
        {
            var query = new ListProducts.ListProductsRequest { Name = name };
            var result = await sender.Send(query);

            return Results.Ok(result.Value);
        })
        .WithName("ListProducts")
        .WithTags("Products-Vertical-Slice");
    }
} 