using Playground.Application.Common.Caching;

namespace Playground.Application.Features.OtherFeature;
public class FeatureCache : BaseCache
{
    protected override TimeSpan RefreshInterval => TimeSpan.FromMinutes(20);
}
