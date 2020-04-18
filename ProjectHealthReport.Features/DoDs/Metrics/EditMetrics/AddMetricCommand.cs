using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.DoDs.Metrics.EditMetrics
{
    public class AddMetricCommand : IRequest<int>
    {
        public MetricDto Metric { get; set; }

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

        public int Response { get; set; }
    }

    public class AddMetricCommandHandler : IExecution<AddMetricCommand, int>
    {
        private readonly ReportDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddMetricCommandHandler(ReportDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task HandleAsync(AddMetricCommand request)
        {
            var metric = _mapper.Map<Metric>(request.Metric);

            await using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                await _dbContext.Metrics.AddAsync(metric);
                await _dbContext.SaveChangesAsync();

                metric.ReplaceThresholds(metric.Thresholds.ToList());

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }

            request.Response = metric.Id;
        }
    }
}