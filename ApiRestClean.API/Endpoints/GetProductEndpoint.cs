using ApiRestClean.Core.Features.Products;
using MediatR;

namespace ApiRestClean.API.Endpoints;

public class GetProductEndpoint : IEndpoint
{
    public void RegisterRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/vertical-slice-get-product/{id:guid}", async (Guid id, ISender sender) =>
        {
            var query = new GetProduct.Request { Id = id };
            var result = await sender.Send(query);

            if (result.IsFailed)
                return Results.NotFound(result.Errors.First().Message);

            return Results.Ok(result.Value);
        })
        .WithName("GetProduct")
        .WithTags("Products-Vertical-Slice");
    }
} 