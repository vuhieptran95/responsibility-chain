using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using ProjectHealthReport.Features.Projects.Commands;
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.Events;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjectsCaching
{
    public partial class GetProjectsCachingQuery
    {
        public class RemoveCache : IPostEvent<AddProjectCommand, int>, IPostEvent<EditProjectMasterDataCommand, int>, IPostEvent<EditProjectNonMasterDataCommand, int>
        {
            private readonly ICacheConfig<GetProjectsCachingQuery> _cacheConfig;
            private readonly IMemoryCache _cache;

            public RemoveCache(ICacheConfig<GetProjectsCachingQuery> cacheConfig, IMemoryCache cache)
            {
                _cacheConfig = cacheConfig;
                _cache = cache;
            }
            public Task HandleAsync(AddProjectCommand request)
            {
                RemoveThisCache();
                
                return Task.CompletedTask;
            }

            public Task HandleAsync(EditProjectMasterDataCommand request)
            {
                RemoveThisCache();
                
                return Task.CompletedTask;
            }

            public Task HandleAsync(EditProjectNonMasterDataCommand request)
            {
                RemoveThisCache();
                
                return Task.CompletedTask;
            }

            private void RemoveThisCache()
            {
                _cache.Remove(_cacheConfig.GetCacheKey(new GetProjectsCachingQuery()));
            }
        }
    }
}