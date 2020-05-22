using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using ProjectHealthReport.Features.Projects.Commands;
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.Events;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjectsMasterCaching
{
    public partial class GetProjectsMasterCachingQuery
    {
        public class RemoveCache : IPostEvent<EditProjectMasterDataCommand, int>,
            IPostEvent<EditProjectNonMasterDataCommand, int>, IPostEvent<AddProjectCommand, int>
        {
            private readonly ICacheConfig<GetProjectsMasterCachingQuery> _cacheConfig;
            private readonly IMemoryCache _memoryCache;

            public RemoveCache(ICacheConfig<GetProjectsMasterCachingQuery> cacheConfig, IMemoryCache memoryCache)
            {
                _cacheConfig = cacheConfig;
                _memoryCache = memoryCache;
            }

            public Task HandleAsync(EditProjectMasterDataCommand request)
            {
                return RemoveCaching();
            }

            public Task HandleAsync(EditProjectNonMasterDataCommand request)
            {
                return RemoveCaching();
            }

            public Task HandleAsync(AddProjectCommand request)
            {
                return RemoveCaching();
            }

            private Task RemoveCaching()
            {
                var query = new GetProjectsMasterCachingQuery();
                var cacheKey = _cacheConfig.GetCacheKey(query);

                _memoryCache.Remove(cacheKey);

                return Task.CompletedTask;
            }
        }
    }
}