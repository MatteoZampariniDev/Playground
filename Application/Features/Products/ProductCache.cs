using Playground.Application.Common.Caching;

namespace Playground.Application.Features.Products;

public class ProductCache : BaseCache
{
    protected override TimeSpan RefreshInterval => TimeSpan.FromHours(1);
}
