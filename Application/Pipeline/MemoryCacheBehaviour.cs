// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.Logging;
using Playground.Application.Common.Caching;
using Playground.Application.Common.Interfaces;
using System.Reflection;

namespace Playground.Application.Pipeline;

public class MemoryCacheBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IRequestCacheService _requestCacheService;
    private readonly ILogger<MemoryCacheBehaviour<TRequest, TResponse>> _logger;

    public MemoryCacheBehaviour(IRequestCacheService requestCacheService,
        ILogger<MemoryCacheBehaviour<TRequest, TResponse>> logger)
    {
        this._requestCacheService = requestCacheService;
        this._logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var cacheAttribute = request!.GetType()
            .GetCustomAttributes()
            .Where(x => x.GetType().GetInterfaces().Contains(typeof(ICacheableAttribute)))
            .FirstOrDefault() as ICacheableAttribute;

        if (cacheAttribute != null)
        {
            this._logger.LogTrace("{Name} is caching with {@Request}", nameof(request), request);
            return await this._requestCacheService.GetOrAddAsync(request, cacheAttribute.CacheType, async () => await next());
        }

        return await next();
    }
}
