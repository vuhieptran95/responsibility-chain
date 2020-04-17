using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.DoDs.Metrics.GetMetrics
{
    public class GetMetricsQuery : IRequest<GetMetricsQuery.Dto>
    {
        public class Dto
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
                    profile.CreateMap<Threshold, ThresholdDto>()
                        .ForMember(des => des.MetricStatusName, opt => opt.MapFrom(src => src.MetricStatus.Name));
                }
            }
        }

        public class Handler : ExecutionHandler<GetMetricsQuery, Dto>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public override async Task HandleAsync(GetMetricsQuery request)
            {
                var metrics = await _dbContext.Metrics.Include(m => m.Thresholds)
                    .ThenInclude(t => t.MetricStatus)
                    .ProjectTo<Dto.MetricDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                var metricsGroup = metrics.GroupBy(m => m.Tool);

                request.Response = new Dto()
                {
                    MetricGroups = metricsGroup.Select(m => new Dto.MetricsGroup()
                        {
                            Tool = m.Key,
                            ToolOrder = m.First().ToolOrder,
                            Metrics = m.OrderBy(mt => mt.Order)
                        })
                        .OrderBy(g => g.ToolOrder)
                };
            }
        }

        public Dto Response { get; set; }
    }
}