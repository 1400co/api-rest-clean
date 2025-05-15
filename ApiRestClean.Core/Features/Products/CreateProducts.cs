using MediatR;
using FluentValidation;
using FluentResults;
using ApiRestClean.Core.Interfaces;
using ApiRestClean.Core.Entities;

namespace ApiRestClean.Core.Features.Products;

public class CreateProduct
{
    public class Request : IRequest<Result<Response>>
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Response
    {
        public Guid Id { get; set; }
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }   

    public class Handler : IRequestHandler<Request, Result<Response>>
    {
        private readonly IProductRepository _productRepository;

        public Handler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }        

        public async Task<Result<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price
            };

            _productRepository.Add(product);

            return Result.Ok(new Response
            {
                Id = product.Id
            });
        }    
    }   
}
