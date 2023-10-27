using Playground.Application.Common.Caching;
using Playground.Application.Features.OtherFeature;

namespace Playground.Application.Features.Products;


[CacheInvalidator<ProductCache>]
[CacheInvalidator<FeatureCache>] // just for testing
public class DeleteProductCommand : IRequest
{

}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(1);
    }
}