namespace Playground.Application.Common.Caching;

public interface ICacheableAttribute
{
    Type CacheType { get; }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class CacheableAttribute<TCache> : Attribute, ICacheableAttribute
    where TCache : BaseCache, new()
{
    public Type CacheType { get; }

    public CacheableAttribute()
    {
        this.CacheType = typeof(TCache);
    }
}
