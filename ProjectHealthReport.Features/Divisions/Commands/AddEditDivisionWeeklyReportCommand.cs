using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Mappings;
using ResponsibilityChain;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Divisions.Commands
{
    public partial class AddEditDivisionWeeklyReportCommand : Request<int>
    {
        public string DivisionName { get; set; }
        public IEnumerable<Dto> DivisionProjectStatuses { get; set; }

        public class Dto 
        {
            public int StatusId { get; set; }
            public int ProjectId { get; set; }
            public string ProjectName { get; set; }
            public string StatusColor { get; set; }
            public string ProjectStatus { get; set; }
            public string Actions { get; set; }
            public int YearWeek { get; set; }
        }
        
        public class AuthorizationConfig: IAuthorizationConfig<AddEditDivisionWeeklyReportCommand>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new []{Resources.DivisionReport}, new []{Actions.Read, Actions.Create, Actions.Update}),
                    (new []{Resources.Project}, new []{Actions.Read})
                };
            }
        }
    }

    public partial class AddEditDivisionWeeklyReportCommand
    {
        public class Handler : IExecution<AddEditDivisionWeeklyReportCommand, int>
        {
            private readonly ReportDbContext _dbContext;

            public Handler(ReportDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task HandleAsync(AddEditDivisionWeeklyReportCommand request)
            {
                var statuses = request.DivisionProjectStatuses.ToList();
                if (!statuses.Any())
                {
                    return;
                }

                var yearWeek = statuses.First().YearWeek;

                var listProjectId = statuses.Select(s => s.ProjectId);

                var projects = (await _dbContext.Projects
                    .Where(p => listProjectId.Contains(p.Id))
                    .Select(p => new
                    {
                        Project = p, Statuses = p.DivisionProjectStatuses.Where(d => d.YearWeek == yearWeek)
                    })
                    .ToListAsync()).Select(p => p.Project).ToList();

                projects.ForEach(p =>
                {
                    var dto = statuses.First(s => s.ProjectId == p.Id);
                    var status = new DivisionProjectStatus(dto.StatusId, yearWeek, dto.ProjectId, dto.StatusColor,
                        dto.ProjectStatus, dto.Actions);

                    p.AddEditDivisionStatus(status);
                });

                await _dbContext.SaveChangesAsync();

                request.Response = 1;
            }
        }
    }
}