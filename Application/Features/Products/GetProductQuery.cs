using Playground.Application.Common.Caching;
using Playground.Application.Features.OtherFeature;

namespace Playground.Application.Features.Products;

[Cacheable<ProductCache>]
[CacheInvalidator<FeatureCache>] // just for testing
public class GetProductQuery : IRequest<SampleResult>
{

}

public class GetAllProductsQueryHandler : IRequestHandler<GetProductQuery, SampleResult>
{
    public Task<SampleResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new SampleResult
        {
            TestMessage = "Query received"
        });
    }
}

public class SampleResult
{
    public string? TestMessage { get; set; }
}
