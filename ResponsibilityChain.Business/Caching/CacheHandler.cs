using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using ResponsibilityChain.Business.RequestContexts;

namespace ResponsibilityChain.Business.Caching
{
    public class CacheHandler<TRequest, TResponse> : Handler<TRequest, TResponse>
    {
        protected readonly IMemoryCache Cache;
        protected readonly CacheConfig<TRequest> CacheConfig;

        public CacheHandler(IMemoryCache cache, CacheConfig<TRequest> cacheConfig)
        {
            Cache = cache;
            CacheConfig = cacheConfig;
        }

        public override async Task<TResponse> HandleAsync(TRequest request)
        {
            if (!CacheConfig.IsEnabled) return await base.HandleAsync(request);
            
            var response = await Cache.GetOrCreateAsync(CacheConfig.GetDefaultCacheKey(request) + CacheConfig.CacheKey,
                async entry =>
                {
                    entry.AbsoluteExpiration = CacheConfig.DateTimeOffset;
                    return await base.HandleAsync(request);
                });

            return response;
        }
    }

    public class CacheConfig<TRequest>
    {
        private readonly RequestContext _requestContext;

        public CacheConfig(RequestContext requestContext)
        {
            _requestContext = requestContext;
        }
        public bool IsEnabled { get; protected set; } = false;
        public string CacheKey { get; set; }

        public string GetDefaultCacheKey(TRequest request)
        {
            var content = JsonSerializer.Serialize(request);
            var requestType = request.GetType().FullName;
            return $"{requestType}_{content}_";
        }

        public DateTimeOffset DateTimeOffset { get; set; }
    }
}