using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Features.Common;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjectsPhrWithWeeklyStatuses
{
    public class GetProjectsPhrWithWeeklyStatusesQuery : IRequest<GetProjectsPhrWithWeeklyStatusesQuery.Dto>
    {
        public int YearWeek { get; set; }
        public Expression<Func<Project, bool>> ResourceFilter { get; set; } = p => true;

        public class Dto
        {
            public int YearWeek { get; set; }
            public List<Project> Projects { get; set; }

            public class Project
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Code { get; set; }
                public string Division { get; set; }
                public string KeyAccountManager { get; set; }
                public string DeliveryResponsibleName { get; set; }
                public List<string> CurrentStatuses { get; set; }
                public List<string> Statuses { get; set; }
            }
        }

        public class Handler : IExecution<GetProjectsPhrWithWeeklyStatusesQuery, Dto>
        {
            private readonly ReportDbContext _dbContext;

            public Handler(ReportDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task HandleAsync(GetProjectsPhrWithWeeklyStatusesQuery request)
            {
                var projects = await _dbContext.Projects
                    .Where(p => p.PhrRequired)
                    .Where(request.ResourceFilter)
                    .OrderBy(p => p.Division)
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        p.Code,
                        p.Division,
                        p.KeyAccountManager,
                        p.DeliveryResponsibleName,
                        IsFilled = p.Statuses.Any(s => s.YearWeek == request.YearWeek),
                        IsMissed = p.WeeklyReportStatuses.Any(s => s.YearWeek == request.YearWeek),
                        MissedTimes = p.WeeklyReportStatuses.Count(s => s.IsDeadlineMissed == true)
                    })
                    .ToListAsync();

                var projectDtos = projects.Select(p =>
                {
                    var listCurrentStatus = new List<string>();
                    var listStatus = new List<string>();

                    if (p.IsMissed)
                    {
                        listCurrentStatus.Add(PhrHelper.MissedStatus);
                    }

                    if (p.MissedTimes > 0)
                    {
                        listStatus.Add(PhrHelper.CreateMissedTimesStatus(p.MissedTimes));
                    }

                    listCurrentStatus.Add(p.IsFilled
                        ? PhrHelper.FilledStatus
                        : PhrHelper.NotFilledStatus);

                    if (listCurrentStatus.Contains(PhrHelper.FilledStatus) &&
                        !listCurrentStatus.Contains(PhrHelper.MissedStatus))
                    {
                        listCurrentStatus.Insert(0, PhrHelper.OnTimeStatus);
                    }

                    return new Dto.Project()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Code = p.Code,
                        Division = p.Division,
                        KeyAccountManager = p.KeyAccountManager,
                        DeliveryResponsibleName = p.DeliveryResponsibleName,
                        CurrentStatuses = listCurrentStatus,
                        Statuses = listStatus
                    };
                }).ToList();

                var dto = new Dto() {Projects = projectDtos, YearWeek = request.YearWeek};

                request.Response = dto;
            }
        }

        public Dto Response { get; set; }
    }
}