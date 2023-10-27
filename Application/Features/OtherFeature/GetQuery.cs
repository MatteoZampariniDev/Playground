using Playground.Application.Common.Caching;
using Playground.Application.Features.Products;

namespace Playground.Application.Features.OtherFeature;

[Cacheable<FeatureCache>]
public class GetQuery : IRequest<SampleResult>
{

}

public class GetQueryHandler : IRequestHandler<GetQuery, SampleResult>
{
    public Task<SampleResult> Handle(GetQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new SampleResult
        {
            TestMessage = "Query received"
        });
    }
}