namespace ApiRestClean.Core.Features.Products;

using MediatR;
using FluentValidation;
using FluentResults;
using ApiRestClean.Core.Interfaces;
using ApiRestClean.Core.Entities;
using ApiRestClean.Core.DTOS;

public class ListProducts
{
    public class ListProductsRequest : IRequest<Result<ListProductsResponse>>
    {
        /// <summary>
        /// Filtro opcional por nombre (contiene, case-insensitive)
        /// </summary>
        public string? Name { get; set; }
    }

    public class ListProductsResponse
    {
        public List<ProductDto> Products { get; set; } = new();
    }

    public class Validator : AbstractValidator<ListProductsRequest>
    {
        public Validator()
        {
            // No hay reglas obligatorias, el filtro es opcional
        }
    }

    public class Handler : IRequestHandler<ListProductsRequest, Result<ListProductsResponse>>
    {
        private readonly IProductRepository _productRepository;

        public Handler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Result<ListProductsResponse>> Handle(ListProductsRequest request, CancellationToken cancellationToken)
        {
            var products = _productRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                var filter = request.Name.Trim().ToLowerInvariant();
                products = products
                    .Where(p => p.Name.ToLowerInvariant().Contains(filter))
                    .ToList();
            }

            var response = new ListProductsResponse
            {
                Products = products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToList()
            };

            return Task.FromResult(Result.Ok(response));
        }
    }
} 