using MediatR;
using FluentValidation;
using FluentResults;
using ApiRestClean.Core.Interfaces;

namespace ApiRestClean.Core.Features.Products;

public class DeleteProduct
{
    public class Request : IRequest<Result<Response>>
    {
        public required Guid Id { get; set; }
    }

    public class Response
    {
        public Guid Id { get; set; }
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
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
            var product = _productRepository.GetById(request.Id);

            if (product == null)
            {
                return Task.FromResult(Result.Fail<Response>("Product not found"));
            }

            _productRepository.Delete(request.Id);

            return Task.FromResult(Result.Ok(new Response
            {
                Id = request.Id
            }));
        }
    }
} 