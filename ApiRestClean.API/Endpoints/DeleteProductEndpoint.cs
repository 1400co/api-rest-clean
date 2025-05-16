using ApiRestClean.Core.Features.Products;
using MediatR;

namespace ApiRestClean.API.Endpoints;

public class DeleteProductEndpoint : IEndpoint
{
    public void RegisterRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/vertical-slice-products/{id:guid}", async (Guid id, ISender sender) =>
        {
            var command = new DeleteProduct.Request { Id = id };
            var result = await sender.Send(command);

            if (result.IsFailed)
                return Results.NotFound(result.Errors.First().Message);

            return Results.NoContent();
        })
        .WithName("DeleteProduct")
        .WithTags("Products-Vertical-Slice");
    }
} 