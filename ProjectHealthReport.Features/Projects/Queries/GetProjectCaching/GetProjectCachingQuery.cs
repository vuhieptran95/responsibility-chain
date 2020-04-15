using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProjectHealthReport.Domains.DomainProxies;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Migrations;
using ResponsibilityChain;
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjectCaching
{
    public class GetProjectCachingQuery : IRequest<Project>
    {
        public int ProjectId { get; set; }
        public Expression<Func<Project, bool>> ResourceFilter { get; set; } = p => true;
        public Project Response { get; set; }

        public class Handler : ExecutionHandlerBase<GetProjectCachingQuery, Project>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public override async Task HandleAsync(GetProjectCachingQuery request)
            {
                await Task.Delay(2000);
                var res = await _dbContext.Projects.AsNoTracking()
                    .Include(p => p.BacklogItems)
                    .Include(p => p.Statuses)
                    .Include(p => p.QualityReports)
                    .Include(p => p.DoDReports)
                    .ThenInclude(d => d.Metric)
                    .Where(request.ResourceFilter)
                    .FirstAsync(p => p.Id == request.ProjectId);
                var resProxy = _mapper.Map<ProjectProxy>(res);
                resProxy.BacklogItems.ToList().ForEach(b => b.Project = null);
                resProxy.Statuses.ToList().ForEach(b => b.Project = null);
                resProxy.BacklogItems.ToList().ForEach(b => b.Project = null);
                
                request.Response = _mapper.Map<Project>(resProxy,
                    options => options.Items[MiscHelper.UserRoleListCtor] = AuthorizationHelper.UserRoleList);
            }
        }

        public class CacheConfig : CacheConfig<GetProjectCachingQuery>
        {
            public CacheConfig(bool isEnabled = false, DateTimeOffset dateTimeOffset = default) : base(isEnabled, dateTimeOffset)
            {
                // IsEnabled = true;
                DateTimeOffset = DateTimeOffset.Now.AddSeconds(20);
            }

            public override string GetCacheKey(GetProjectCachingQuery request)
            {
                return typeof(GetProjectCachingQuery).FullName + "_" + request.ProjectId;
            }
        }
    }
}