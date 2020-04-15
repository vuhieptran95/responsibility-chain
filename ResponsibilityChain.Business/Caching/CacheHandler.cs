using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using ResponsibilityChain.Business.RequestContexts;

namespace ResponsibilityChain.Business.Caching
{
    public class CacheHandler<TRequest, TResponse> : Handler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        protected readonly IMemoryCache Cache;
        protected readonly CacheConfig<TRequest> CacheConfig;

        public CacheHandler(IMemoryCache cache, CacheConfig<TRequest> cacheConfig)
        {
            Cache = cache;
            CacheConfig = cacheConfig;
        }

        public override async Task HandleAsync(TRequest request)
        {
            if (!CacheConfig.IsCacheEnabled)
            {
                await base.HandleAsync(request);
            }
            else
            {
                request.Response = await Cache.GetOrCreateAsync(CacheConfig.GetCacheKey(request),
                    async entry =>
                    {
                        entry.AbsoluteExpiration = CacheConfig.CacheDateTimeOffset;
                        await base.HandleAsync(request);
                        return request.Response;
                    });
            }
        }
    }

    public class CacheConfig<TRequest>
    {
        protected bool IsEnabled;
        protected DateTimeOffset DateTimeOffset;

        public CacheConfig(bool isEnabled = false, DateTimeOffset dateTimeOffset = default)
        {
            DateTimeOffset = dateTimeOffset;
            IsEnabled = isEnabled;
        }

        public bool IsCacheEnabled
        {
            get => IsEnabled;
        }

        public virtual string GetCacheKey(TRequest request)
        {
            throw new NotImplementedException();
        }

        public DateTimeOffset CacheDateTimeOffset
        {
            get => DateTimeOffset;
        }
    }
}