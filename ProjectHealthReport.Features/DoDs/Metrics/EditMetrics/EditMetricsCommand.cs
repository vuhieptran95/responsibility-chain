﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.DomainProxies;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Mappings;
using ResponsibilityChain;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.DoDs.Metrics.EditMetrics
{
    public class EditMetricsCommand : Request<int>
    {
        public IEnumerable<MetricsGroup> MetricGroups { get; set; }

        public class MetricsGroup
        {
            public string Tool { get; set; }
            public int ToolOrder { get; set; }
            public IEnumerable<MetricDto> Metrics { get; set; }
        }

        public class MetricDto : IMapTo<MetricProxy>, IMapFrom<MetricProxy>
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ValueType { get; set; }
            public string Unit { get; set; }
            public int Order { get; set; }
            public string Tool { get; set; }
            public int ToolOrder { get; set; }
            public string SelectValues { get; set; }
            public IEnumerable<ThresholdDto> Thresholds { get; set; }

        }

        public class ThresholdDto: IMapFrom<Threshold>, IMapTo<ThresholdProxy>
        {
            public int MetricStatusId { get; set; }
            public int MetricId { get; set; }
            public decimal? UpperBound { get; set; }
            public decimal? LowerBound { get; set; }
            public string UpperBoundOperator { get; set; }
            public string LowerBoundOperator { get; set; }
            public bool IsRange { get; set; }
            public string Value { get; set; }
            public string MetricStatusName { get; set; }

        }
        
        public class AuthorizationConfig : IAuthorizationConfig<EditMetricsCommand>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new[] {Resources.Metrics}, new[] {Actions.Read, Actions.Create, Actions.Update}),
                };
            }
        }

        public class Handler : IExecution<EditMetricsCommand, int>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task HandleAsync(EditMetricsCommand request)
            {
                var metricDtos = request.MetricGroups.SelectMany(g => g.Metrics, (g, m) =>
                {
                    m.Tool = g.Tool;
                    m.ToolOrder = g.ToolOrder;
                    return m;
                }).ToList();

                var listMetricId = metricDtos.Select(m => m.Id).ToList();

                var metricsInDb = _dbContext.Metrics.Include(m => m.Thresholds).Where(m => listMetricId.Contains(m.Id)).ToList();
                
                metricDtos.ForEach(dto =>
                {
                    var metric = metricsInDb.First(m => m.Id == dto.Id);
                    metric.UpdateValue(dto.Id, dto.Name, dto.ValueType, dto.Unit, dto.Tool, dto.SelectValues, dto.Order, dto.ToolOrder);

                    var thresholdsToUpdate = dto.Thresholds.Select(t => new Threshold(t.MetricStatusId, t.MetricId,
                        t.UpperBound, t.LowerBound, t.UpperBoundOperator, t.LowerBoundOperator, t.IsRange, t.Value)).ToList();
                    
                    metric.ReplaceThresholds(thresholdsToUpdate);
                });
                
                await _dbContext.SaveChangesAsync();

                request.Response = 1;
            }
        }

    }
}