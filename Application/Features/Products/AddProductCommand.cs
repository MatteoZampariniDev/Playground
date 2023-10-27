using Playground.Application.Common.Caching;

namespace Playground.Application.Features.Products;

[CacheInvalidator<ProductCache>]
public class AddProductCommand : IRequest<SampleResult>
{

}

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, SampleResult>
{
    public Task<SampleResult> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new SampleResult
        {
            TestMessage = "Query received"
        });
    }
}