using Microsoft.Extensions.Logging;
using Playground.Application.Common.Caching;
using Playground.Application.Common.Interfaces;
using System.Reflection;

namespace Playground.Application.Pipeline;
public class CacheInvalidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IRequestCacheService _requestCacheService;
    private readonly ILogger<CacheInvalidationBehaviour<TRequest, TResponse>> _logger;

    public CacheInvalidationBehaviour(IRequestCacheService requestCacheService,
        ILogger<CacheInvalidationBehaviour<TRequest, TResponse>> logger)
    {
        this._requestCacheService = requestCacheService;
        this._logger = logger;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();

        this._logger.LogTrace("{Name} cache expire with {@Request}", nameof(request), request);

        var cacheAttributes = request!.GetType()
            .GetCustomAttributes()
            .Where(x => x.GetType().GetInterfaces().Contains(typeof(ICacheInvalidatorAttribute))).Cast<ICacheInvalidatorAttribute>();

        if (cacheAttributes != null)
        {
            foreach (var attr in cacheAttributes)
            {
                this._requestCacheService.Refresh(attr.CacheType);
            }
        }

        return response;
    }
}
