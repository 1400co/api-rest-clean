using ApiRestClean.API.Endpoints;
using ApiRestClean.Core.Entities;
using ApiRestClean.Core.Interfaces;

namespace ApiRestClean.API.Endpoints;

public class ProductEndpoints : IEndpoint
{
    public void RegisterRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products").WithTags("Products");

        group.MapGet("/", (IProductRepository repo) =>
        {
            var products = repo.GetAll();
            return Results.Ok(products);
        });

        group.MapGet("/{id:guid}", (Guid id, IProductRepository repo) =>
        {
            var product = repo.GetById(id);
            return product is null ? Results.NotFound() : Results.Ok(product);
        });

        group.MapPost("/", (Product product, IProductRepository repo) =>
        {
            repo.Add(product);
            return Results.Created($"/products/{product.Id}", product);
        });

        group.MapPut("/{id:guid}", (Guid id, Product updated, IProductRepository repo) =>
        {
            var existing = repo.GetById(id);
            if (existing is null) return Results.NotFound();

            existing.Name = updated.Name;
            existing.Price = updated.Price;
            repo.Update(existing);

            return Results.Ok(existing);
        });

        group.MapDelete("/{id:guid}", (Guid id, IProductRepository repo) =>
        {
            var existing = repo.GetById(id);
            if (existing is null) return Results.NotFound();

            repo.Delete(id);
            return Results.NoContent();
        });
    }
}
