using Playground.Application.Common.Caching;

namespace Playground.Application.Features.Products;


[Cacheable<ProductCache>]
public class GetProductQuery : IRequest<SampleResult>
{

}

[Cacheable<ProductCache>]
public class GetAllProductsQuery : IRequest<SampleResult>
{

}

public class GetAllProductsQueryHandler : IRequestHandler<GetProductQuery, SampleResult>,
    IRequestHandler<GetAllProductsQuery, SampleResult>
{
    public Task<SampleResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new SampleResult
        {
            TestMessage = "Query received"
        });
    }

    public Task<SampleResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
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
