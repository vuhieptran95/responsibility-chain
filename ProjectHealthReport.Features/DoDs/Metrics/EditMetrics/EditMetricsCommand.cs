using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.DoDs.Metrics.EditMetrics
{
    public class EditMetricsCommand : IRequest<int>
    {
        public IEnumerable<MetricsGroup> MetricGroups { get; set; }

        public class MetricsGroup
        {
            public string Tool { get; set; }
            public int ToolOrder { get; set; }
            public IEnumerable<MetricDto> Metrics { get; set; }
        }

        public class MetricDto : IMapFrom<Metric>
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

            public void MappingFrom(Profile profile)
            {
                profile.CreateMap<MetricDto, Metric>()
                    .ConstructUsing((dto, context) => new Metric(dto.Id, dto.Name, dto.ValueType, dto.Unit, dto.Tool,
                        dto.SelectValues, dto.Order, dto.ToolOrder,
                        context.Mapper.Map<ICollection<Threshold>>(dto.Thresholds), null));
            }
        }

        public class ThresholdDto : IMapFrom<Threshold>
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

            public void MappingFrom(Profile profile)
            {
                profile.CreateMap<ThresholdDto, Threshold>()
                    .ConstructUsing(dto => new Threshold(dto.MetricStatusId, dto.MetricId, dto.UpperBound,
                        dto.LowerBound, dto.UpperBoundOperator, dto.LowerBoundOperator, dto.IsRange, dto.Value, null,
                        null));
            }
        }

        public class Handler : ExecutionHandlerBase<EditMetricsCommand, int>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public override async Task<int> HandleAsync(EditMetricsCommand request)
            {
                var metricDtos = request.MetricGroups.SelectMany(g => g.Metrics, (g, m) =>
                {
                    m.Tool = g.Tool;
                    m.ToolOrder = g.ToolOrder;
                    return m;
                });
                var metrics = _mapper.Map<IEnumerable<Metric>>(metricDtos).ToList();

                var thresholds = metrics.SelectMany(m => m.Thresholds, (m, t) => t).ToList();

                var thresholdsInDb = await _dbContext.Thresholds.AsNoTracking().ToListAsync();

                var listAddNew = thresholds.Except(thresholdsInDb, Threshold.ThresholdComparer);
                var listRemove = thresholdsInDb.Except(thresholds, Threshold.ThresholdComparer);
                var listUpdate = thresholds.Intersect(thresholdsInDb, Threshold.ThresholdComparer);

                _dbContext.Metrics.UpdateRange(metrics);
                _dbContext.Thresholds.UpdateRange(listUpdate);
                _dbContext.Thresholds.RemoveRange(listRemove);
                await _dbContext.Thresholds.AddRangeAsync(listAddNew);

                await _dbContext.SaveChangesAsync();

                return 1;
            }
        }
    }
}