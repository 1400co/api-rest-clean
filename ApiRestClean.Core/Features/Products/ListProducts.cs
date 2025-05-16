namespace ApiRestClean.Core.Features.Products;

using MediatR;
using FluentValidation;
using FluentResults;
using ApiRestClean.Core.Interfaces;
using ApiRestClean.Core.Entities;
using ApiRestClean.Core.DTOS;

public class ListProducts
{
    public class Request : IRequest<Result<Response>>
    {
        /// <summary>
        /// Filtro opcional por nombre (contiene, case-insensitive)
        /// </summary>
        public string? Name { get; set; }
    }

    public class Response
    {
        public List<ProductDto> Products { get; set; } = new();
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            // No hay reglas obligatorias, el filtro es opcional
        }
    }

    public class Handler : IRequestHandler<Request, Result<Response>>
    {
        private readonly IProductRepository _productRepository;

        public Handler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Result<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            var products = _productRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                var filter = request.Name.Trim().ToLowerInvariant();
                products = products
                    .Where(p => p.Name.ToLowerInvariant().Contains(filter))
                    .ToList();
            }

            var response = new Response
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