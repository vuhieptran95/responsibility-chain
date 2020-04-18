using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Mappings;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Divisions.Queries
{
    public class GetDivisionWeeklyReportQuery : IRequest<GetDivisionWeeklyReportQuery.Dto>
    {
        [Required] public string DivisionName { get; set; }
        public int SelectedYearWeek { get; set; }

        public class Handler : IExecution<GetDivisionWeeklyReportQuery, Dto>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task HandleAsync(GetDivisionWeeklyReportQuery request)
            {
                if (request.DivisionName == "AMS247")
                {
                    request.DivisionName = "AMS 24/7";
                }
                
                var divisionProjectStatusDto = (await _dbContext.Projects
                        .Where(p => p.Division == request.DivisionName
                                    && p.DmrRequired)
                        .Select(p => new
                        {
                            Project = p,
                            Status = p.DivisionProjectStatuses.SingleOrDefault(s =>
                                s.YearWeek == request.SelectedYearWeek),
                        })
                        .ToListAsync())
                    .Where(i => IsProjectDisplayed(i.Project, request.SelectedYearWeek))
                    .Select(p => new Dto.DivisionProjectStatusDto()
                    {
                        ProjectId = p.Project.Id,
                        Code = p.Project.Code,
                        ProjectName = p.Project.Name,
                        Actions = p.Status?.Actions,
                        StatusColor = p.Status?.StatusColor,
                        ProjectStatus = p.Status?.ProjectStatus,
                        StatusId = p.Status?.Id ?? 0,
                        YearWeek = request.SelectedYearWeek
                    })
                    .ToList();

                var result = new Dto
                {
                    DivisionName = request.DivisionName,
                    YearWeek = request.SelectedYearWeek,
                    DivisionProjectStatuses = divisionProjectStatusDto
                };

                request.Response = result;
            }

            private bool IsProjectDisplayed(Project project, int selectedWeek)
            {
                if (project.DmrRequiredFrom == null)
                {
                    throw new InvalidOperationException(
                        $"Project is marked as DMR Required but Start time is not specified. Project name: {project.Name} - code: {project.Code}");
                }

                var dmrRequiredTo = project.DmrRequiredTo;
                if (project.DmrRequiredTo == null)
                {
                    dmrRequiredTo = new DateTime(2200, 1, 1);
                }

                var dmrRequiredFromWeek = TimeHelper.GetYearWeek(project.DmrRequiredFrom.Value);
                var dmrRequiredToWeek = TimeHelper.GetYearWeek(dmrRequiredTo.Value);

                if (dmrRequiredFromWeek <= selectedWeek && selectedWeek <= dmrRequiredToWeek)
                {
                    return true;
                }

                return false;
            }
        }

        public class Dto
        {
            public string DivisionName { get; set; }
            public int YearWeek { get; set; }
            public List<DivisionProjectStatusDto> DivisionProjectStatuses { get; set; }

            public class DivisionProjectStatusDto : IMapFrom<DivisionProjectStatus>
            {
                public int StatusId { get; set; }
                public int ProjectId { get; set; }
                public string Code { get; set; }
                public string ProjectName { get; set; }
                public string StatusColor { get; set; }
                public string ProjectStatus { get; set; }
                public string Actions { get; set; }
                public int YearWeek { get; set; }
            }
        }

        public Dto Response { get; set; }
    }
}