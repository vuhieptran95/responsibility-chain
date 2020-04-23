using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Mappings;
using ResponsibilityChain;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.DoDs.AddEditDoDReport
{
    public partial class AddDoDReportCommand : Request<int>
    {
        public IEnumerable<DoDReportDto> DodReports { get; set; }
        
        public class DoDReportDto : IMapFrom<DoDReport>
        {
            public int ProjectId { get; set; }
            public int MetricId { get; set; }
            public string Value { get; set; }
            public int YearWeek { get; set; }
            public string LinkToReport { get; set; }
            public string ReportFileName { get; set; }
            
            public Metric Metric { get; set; }
            public Project Project { get; set; }

            public void MappingFrom(Profile profile)
            {
                profile.CreateMap<DoDReportDto, DoDReport>()
                    .ConstructUsing(dto => new DoDReport(dto.ProjectId, dto.MetricId, dto.YearWeek, dto.Value,
                        dto.LinkToReport, dto.ReportFileName, dto.Project, dto.Metric));
            }
        }
        
        public class AuthorizationConfig: IAuthorizationConfig<AddDoDReportCommand>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new []{Resources.DoDReport}, new []{Actions.Read, Actions.Create, Actions.Update}),
                    (new []{Resources.Project}, new []{Actions.Read})
                };
            }
        }
    }

    public partial class AddDoDReportCommand
    {
        public class Handler : IExecution<AddDoDReportCommand, int>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task HandleAsync(AddDoDReportCommand request)
            {
                var listDtos = request.DodReports.ToList();

                var listProjectId = listDtos.Select(d => d.ProjectId).Distinct();
                var listProject = await _dbContext.Projects.Where(p => listProjectId.Contains(p.Id)).ToListAsync();

                var listMetricId = listDtos.Select(d => d.MetricId).Distinct();
                var listMetric = await _dbContext.Metrics.Where(m => listMetricId.Contains(m.Id)).ToListAsync();

                listDtos.ForEach(d =>
                {
                    d.Project = listProject.First(p => p.Id == d.ProjectId);
                    d.Metric = listMetric.First(m => m.Id == d.MetricId);
                });

                var dodReports = _mapper.Map<IEnumerable<DoDReport>>(listDtos);

                await _dbContext.DoDReports.AddRangeAsync(dodReports);
                await _dbContext.SaveChangesAsync();

                request.Response = 1;
            }
        }
    }
}