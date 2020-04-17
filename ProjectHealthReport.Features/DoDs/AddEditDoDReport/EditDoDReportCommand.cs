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
using ResponsibilityChain.Business.Validations;

namespace ProjectHealthReport.Features.DoDs.AddEditDoDReport
{
    public partial class EditDoDReportCommand
    {
        public class EditOneProjectAtATime : PreValidation<EditDoDReportCommand, int>
        {
            public override Task HandleAsync(EditDoDReportCommand request)
            {
                if (request.DodReports.Count > 0)
                {
                    var projectId = request.DodReports[0].ProjectId;
                    if (request.DodReports.Any(d => d.ProjectId != projectId))
                    {
                        throw new Exception("Edit 1 project at a time");
                    }
                    
                }
                return Task.CompletedTask;
            }
        }

        public int Response { get; set; }
    }

    public partial class EditDoDReportCommand : IRequest<int>
    {
        public List<DoDReportDto> DodReports { get; set; }

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

        public class Handler : ExecutionHandler<EditDoDReportCommand, int>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public override async Task HandleAsync(EditDoDReportCommand request)
            {
                if (request.DodReports.Count < 1) return;

                var projectId = request.DodReports[0].ProjectId;
                var yearWeek = request.DodReports[0].YearWeek;
                var listMetricId = request.DodReports.Select(d => d.MetricId).Distinct();

                var project = await _dbContext.Projects.AsNoTracking().FirstAsync(p => p.Id == projectId);
                var listMetric = await _dbContext.Metrics.AsNoTracking().Where(m => listMetricId.Contains(m.Id))
                    .ToListAsync();
                request.DodReports.ForEach(d =>
                {
                    d.Project = project;
                    d.Metric = listMetric.First(m => m.Id == d.MetricId);
                });

                var dodReportsInDb = await _dbContext.DoDReports
                    .AsNoTracking()
                    .Where(r => r.ProjectId == projectId && r.YearWeek == yearWeek)
                    .ToListAsync();

                var dodReports = _mapper.Map<List<DoDReport>>(request.DodReports);

                var recordsToAdd = dodReports.Except(dodReportsInDb, DoDReport.DoDComparer);
                var recordsToRemove = dodReportsInDb.Except(dodReports, DoDReport.DoDComparer);
                var recordsToUpdate = dodReports.Intersect(dodReportsInDb, DoDReport.DoDComparer);

                await _dbContext.DoDReports.AddRangeAsync(recordsToAdd);
                _dbContext.DoDReports.RemoveRange(recordsToRemove);
                _dbContext.UpdateRange(recordsToUpdate);

                await _dbContext.SaveChangesAsync();
                request.Response = 1;
            }
        }
    }
}