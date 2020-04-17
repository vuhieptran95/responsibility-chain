using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Divisions.Commands
{
    public class AddEditDivisionWeeklyReportCommand : IRequest<int>
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
        
        public class Handler: ExecutionHandler<AddEditDivisionWeeklyReportCommand, int>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public override async Task HandleAsync(AddEditDivisionWeeklyReportCommand request)
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
                    var status = new DivisionProjectStatus(dto.StatusId, yearWeek, dto.ProjectId, dto.StatusColor, dto.ProjectStatus, dto.Actions);
                    
                    p.AddEditDivisionStatus(status);
                });
                
                await _dbContext.SaveChangesAsync();

                request.Response = 1;
            }
        }

        public int Response { get; set; }
    }
}