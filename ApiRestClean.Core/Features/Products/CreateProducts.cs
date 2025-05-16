using MediatR;
using FluentValidation;
using FluentResults;
using ApiRestClean.Core.Interfaces;
using ApiRestClean.Core.Entities;

namespace ApiRestClean.Core.Features.Products;

public class CreateProduct
{
    public class CreateProductRequest : IRequest<Result<CreateProductResponse>>
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateProductResponse
    {
        public Guid Id { get; set; }
    }

    public class Validator : AbstractValidator<CreateProductRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }   

    public class Handler : IRequestHandler<CreateProductRequest, Result<CreateProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public Handler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }        

        public async Task<Result<CreateProductResponse>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price
            };

            _productRepository.Add(product);

            return Result.Ok(new CreateProductResponse
            {
                Id = product.Id
            });
        }    
    }   
}
