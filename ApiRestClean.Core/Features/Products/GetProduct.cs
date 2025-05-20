using MediatR;
using FluentValidation;
using FluentResults;
using ApiRestClean.Core.Interfaces;
using ApiRestClean.Core.Entities;

namespace ApiRestClean.Core.Features.Products;

public class GetProduct
{
    public class GetProductRequest : IRequest<Result<GetProductResponse>>
    {
        public required Guid Id { get; set; }
    }

    public class GetProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
    }

    public class Validator : AbstractValidator<GetProductRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<GetProductRequest, Result<GetProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public Handler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Result<GetProductResponse>> Handle(GetProductRequest request, CancellationToken cancellationToken)
        {
            var product = _productRepository.GetById(request.Id);

            if (product == null)
            {
                return Task.FromResult(Result.Fail<GetProductResponse>("Product not found"));
            }

            var response = new GetProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };

            return Task.FromResult(Result.Ok(response));
        }
    }
} 