using LazyCache;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Playground.Application.Common.Caching;

namespace Playground.Application.Common.Interfaces;
public interface IRequestCacheService
{
    Task<TResponse> GetOrAddAsync<TRequest, TResponse>(TRequest request, Type cacheType, Func<Task<TResponse>> addItemFactory);

    void Refresh(Type cacheType);
}


public class RequestCacheService : IRequestCacheService
{
    private readonly IAppCache _appCache;
    private readonly IServiceProvider _serviceProvider;

    public RequestCacheService(IAppCache cache, IServiceProvider serviceProvider)
    {
        this._appCache = cache;
        this._serviceProvider = serviceProvider;
    }

    public async Task<TResponse> GetOrAddAsync<TRequest, TResponse>(TRequest request, Type cacheType, Func<Task<TResponse>> addItemFactory)
    {
        var cache = this._serviceProvider.GetRequiredService(cacheType) as BaseCache;

#if DEBUG // testing only, will be removed
        var currentCached = await this._appCache.GetAsync<TResponse>(RequestAsCacheableString(request));
#endif

        return await this._appCache.GetOrAddAsync(
            RequestAsCacheableString(request),
            addItemFactory,
            cache!.MemoryCacheEntryOptions);
    }

    public void Refresh(Type cacheType)
    {
        var cache = this._serviceProvider.GetRequiredService(cacheType) as BaseCache;

        this._appCache.Remove(cache!.Key);
        cache.Refresh();
    }

    private static string RequestAsCacheableString<TRequest>(TRequest cacheableRequest)
    {
        return $"{typeof(TRequest).FullName}_{ToJson(cacheableRequest!, Formatting.None)}";
    }

    private static string ToJson(object obj, Formatting formatting = Formatting.Indented)
    {
        return JsonConvert.SerializeObject(obj, formatting);
    }
}