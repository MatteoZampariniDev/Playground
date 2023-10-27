namespace Playground.Application.Common.Caching;

public interface ICacheInvalidatorAttribute
{
    Type CacheType { get; }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class CacheInvalidatorAttribute<TCache> : Attribute, ICacheInvalidatorAttribute
    where TCache : BaseCache, new()
{
    public Type CacheType { get; }

    public CacheInvalidatorAttribute()
    {
        this.CacheType = typeof(TCache);
    }
}
