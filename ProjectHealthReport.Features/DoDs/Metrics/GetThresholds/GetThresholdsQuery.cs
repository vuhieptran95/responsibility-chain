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

namespace ProjectHealthReport.Features.DoDs.Metrics.GetThresholds
{
    public class GetThresholdsQuery : IRequest<GetThresholdsQuery.Dto>
    {
        public class Dto
        {
            public IEnumerable<ThresholdDto> Thresholds { get; set; }

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

        public class Handler : ExecutionHandlerBase<GetThresholdsQuery,Dto>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public override async Task HandleAsync(GetThresholdsQuery request)
            {
                var projectMetrics = await _dbContext.Thresholds
                    .ProjectTo<Dto.ThresholdDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                request.Response = new Dto() {Thresholds = projectMetrics};
            }
        }

        public Dto Response { get; set; }
    }
}