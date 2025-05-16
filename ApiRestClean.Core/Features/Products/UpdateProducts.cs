using MediatR;
using FluentValidation;
using FluentResults;
using ApiRestClean.Core.Interfaces;
using ApiRestClean.Core.Entities;

namespace ApiRestClean.Core.Features.Products;

public class UpdateProduct
{
    public class UpdateProductRequest : IRequest<Result<UpdateProductResponse>>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateProductResponse
    {
        public Guid Id { get; set; }
    }

    public class Validator : AbstractValidator<UpdateProductRequest>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<UpdateProductRequest, Result<UpdateProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public Handler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<UpdateProductResponse>> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var product =  _productRepository.GetById(request.Id);

            if (product == null)
            {
                return Result.Fail("Product not found");
            }

            product.Name = request.Name;
            product.Price = request.Price;

             _productRepository.Update(product);

            return Result.Ok(new UpdateProductResponse
            {
                Id = product.Id
            });
        }
    }
}
