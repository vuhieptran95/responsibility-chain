using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using ProjectHealthReport.Features.Projects.Commands;
using ProjectHealthReport.Features.WeeklyReports.Commands.AddEditWeeklyReportPhr;
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.Events;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjectCaching
{
    public partial class GetProjectCachingQuery
    {
        public class RemoveProjectCache : IPostEvent<EditProjectNonMasterDataCommand, int>,
            IPostEvent<EditProjectMasterDataCommand, int>, IPostEvent<AddEditWeeklyReportPhrCommand, int>
        {
            private readonly ICacheConfig<GetProjectCachingQuery> _cacheConfig;
            private readonly IMemoryCache _cache;

            public RemoveProjectCache(ICacheConfig<GetProjectCachingQuery> cacheConfig, IMemoryCache cache)
            {
                _cacheConfig = cacheConfig;
                _cache = cache;
            }

            public Task HandleAsync(EditProjectNonMasterDataCommand request)
            {
                var cacheKey = _cacheConfig.GetCacheKey(new GetProjectCachingQuery() {ProjectId = request.Id});
                _cache.Remove(cacheKey);

                return Task.CompletedTask;
            }

            public Task HandleAsync(EditProjectMasterDataCommand request)
            {
                var cacheKey = _cacheConfig.GetCacheKey(new GetProjectCachingQuery() {ProjectId = request.Id});
                _cache.Remove(cacheKey);

                return Task.CompletedTask;
            }

            public Task HandleAsync(AddEditWeeklyReportPhrCommand request)
            {
                var cacheKey = _cacheConfig.GetCacheKey(new GetProjectCachingQuery() {ProjectId = request.Report.ProjectId});
                _cache.Remove(cacheKey);

                return Task.CompletedTask;
            }
        }
    }
}