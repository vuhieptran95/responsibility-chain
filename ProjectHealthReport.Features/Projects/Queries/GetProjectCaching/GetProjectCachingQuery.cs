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
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjectCaching
{
    public class GetProjectCachingQuery : IRequest<CacheResponse<GetProjectCachingQuery.WeeklyData>>
    {
        public int ProjectId { get; set; }
        public CacheResponse<WeeklyData> Response { get; set; } = new CacheResponse<WeeklyData>();

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

        public class Handler : ExecutionHandler<GetProjectCachingQuery, CacheResponse<WeeklyData>>
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

            public override async Task HandleAsync(GetProjectCachingQuery request)
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

                request.Response.SetResponse(data);
            }
        }

        public class CacheConfig : CacheConfig<GetProjectCachingQuery>
        {
            public CacheConfig(bool isEnabled = false, DateTimeOffset dateTimeOffset = default) : base(isEnabled,
                dateTimeOffset)
            {
                IsEnabled = true;
                DateTimeOffset = DateTimeOffset.Now.AddDays(1);
            }

            public override string GetCacheKey(GetProjectCachingQuery request)
            {
                return typeof(GetProjectCachingQuery).FullName + "_" + request.ProjectId;
            }
        }
    }
}