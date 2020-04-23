using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using MessagePack;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using ProjectHealthReport.Domains.DomainProxies;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Migrations;
using ProjectHealthReport.Features.Common;
using ResponsibilityChain;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjectCaching
{
    public class GetProjectCachingQuery : Request<CacheResponse<GetProjectCachingQuery.WeeklyData>>
    {
        public int ProjectId { get; set; }

        [MessagePackObject]
        public class WeeklyData
        {
            public WeeklyData()
            {
                Statuses = new List<StatusProxy>();
                BacklogItems = new List<BacklogItemProxy>();
                QualityReports = new List<QualityReportProxy>();
            }

            [Key(0)] public IEnumerable<StatusProxy> Statuses { get; set; }
            [Key(1)] public IEnumerable<BacklogItemProxy> BacklogItems { get; set; }
            [Key(2)] public IEnumerable<QualityReportProxy> QualityReports { get; set; }
        }

        public class AuthorizationConfig : IAuthorizationConfig<GetProjectCachingQuery>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new[] {Resources.Project, Resources.BacklogItem, Resources.QualityReport, Resources.ProjectStatus, Resources.DoDReport},
                        new[] {Actions.Read}),
                };
            }
        }

        public class Handler : IExecution<GetProjectCachingQuery, CacheResponse<WeeklyData>>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly IConfiguration _configuration;

            public Handler(ReportDbContext dbContext, IMapper mapper, IConfiguration configuration)
            {
                _dbContext = dbContext;
                _mapper = mapper;
                _configuration = configuration;
            }

            public async Task HandleAsync(GetProjectCachingQuery request)
            {
                await Task.Delay(2000);

                var data = new WeeklyData();
                using (var con = new SqlConnection(_configuration.GetConnectionString("ProjectHealthReport")))
                {
                    await con.OpenAsync();
                    var sql = @"
SELECT *
FROM Statuses
WHERE ProjectId = @ProjectId;


SELECT *
FROM BacklogItems
WHERE ProjectId = @ProjectId;


SELECT *
FROM QualityReports
WHERE ProjectId = @ProjectId;";

                    using (var results = await con.QueryMultipleAsync(sql, new {ProjectId = request.ProjectId}))
                    {
                        data.Statuses =
                            (await results.ReadAsync<StatusProxy>());
                        data.BacklogItems =
                            (await results.ReadAsync<BacklogItemProxy>());
                        data.QualityReports =
                            (await results.ReadAsync<QualityReportProxy>());
                    }

                    await con.CloseAsync();
                }

                request.Response = new CacheResponse<WeeklyData>();
                request.Response.SetResponse(data);
            }
        }

        public class CacheConfig : ICacheConfig<GetProjectCachingQuery>
        {
            public bool IsCacheEnabled { get; } = true;
            public DateTimeOffset CacheDateTimeOffset { get; } = DateTimeOffset.Now.AddDays(1);

            public string GetCacheKey(GetProjectCachingQuery request)
            {
                return typeof(GetProjectCachingQuery).FullName + "_" + request.ProjectId;
            }
        }
    }
}