using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Mappings;
using ResponsibilityChain;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjectIndexPhr
{
    public class GetProjectIndexPhrQuery : Request<GetProjectIndexPhrQuery.Dto>
    {
        public Expression<Func<Domains.Domains.Project, bool>> ResourceFilter { get; set; } = p => true;
        
        public class AuthorizationConfig : IAuthorizationConfig<GetProjectIndexPhrQuery>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new[] {Resources.Project},
                        new[] {Actions.Read}),
                };
            }
        }

        public class Hander : IExecution<GetProjectIndexPhrQuery, Dto>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly IOptions<BusinessRules> _rules;

            public Hander(ReportDbContext dbContext, IMapper mapper, IOptions<BusinessRules> rules)
            {
                _dbContext = dbContext;
                _mapper = mapper;
                _rules = rules;
            }

            public async Task HandleAsync(GetProjectIndexPhrQuery request)
            {
                var dto = new Dto();

                var currentYearWeek = TimeHelper.GetYearWeek(DateTime.Now);
                var lastYearWeek = TimeHelper.GetLastYearWeek(currentYearWeek);

                var fromDay = TimeHelper.GetIsoDayOfWeek(_rules.Value.ShowWarningIfProjectNotYetSubmitReport.FromDay);
                var fromHour = _rules.Value.ShowWarningIfProjectNotYetSubmitReport.FromHour;
                var toDay = TimeHelper.GetIsoDayOfWeek(_rules.Value.ShowWarningIfProjectNotYetSubmitReport.ToDay);
                var toHour = _rules.Value.ShowWarningIfProjectNotYetSubmitReport.ToHour;
                var noOfWeekBetween = _rules.Value.ShowWarningIfProjectNotYetSubmitReport.NumberOfWeekBetween;

                var nextYearWeek = TimeHelper.GetNextXYearWeek(currentYearWeek, noOfWeekBetween);

                var condition = TimeHelper.IsDateTimeBetweenDays(DateTime.Now, fromDay, fromHour, currentYearWeek,
                    toDay,
                    toHour, nextYearWeek);

                var firstWorkingDayOfLastWeek = TimeHelper.GetFirstWorkingDateOfWeek(
                    TimeHelper.CalculateYear(lastYearWeek),
                    TimeHelper.CalculateWeek(lastYearWeek));

                if (condition)
                {
                    dto.Projects = await _dbContext.Projects
                        .Where(p => p.PhrRequired)
                        .Where(request.ResourceFilter)
                        .Select(
                            r => new Dto.Project
                            {
                                Id = r.Id,
                                Name = r.Name,
                                Division = r.Division,
                                KeyAccountManager = r.KeyAccountManager,
                                DeliveryResponsibleName = r.DeliveryResponsibleName,
                                ProjectStartDate = r.ProjectStartDate,
                                IsNotSubmitted =
                                    (r.Statuses.SingleOrDefault(s => s.YearWeek == lastYearWeek) == null) &&
                                    r.PhrRequiredFrom < firstWorkingDayOfLastWeek
                            }
                        )
                        .OrderBy(p => p.Division)
                        .ToListAsync();
                }
                else
                {
                    dto.Projects = await _dbContext.Projects
                        .Where(p => p.PhrRequired)
                        .Where(request.ResourceFilter)
                        .Select(
                            r => new Dto.Project
                            {
                                Id = r.Id,
                                Name = r.Name,
                                Division = r.Division,
                                KeyAccountManager = r.KeyAccountManager,
                                DeliveryResponsibleName = r.DeliveryResponsibleName,
                                ProjectStartDate = r.ProjectStartDate,
                            }
                        )
                        .OrderBy(p => p.Division)
                        .ToListAsync();
                }

                request.Response = dto;
            }
        }

        public class Dto
        {
            public IEnumerable<Project> Projects { get; set; }

            public class Project : IMapFrom<Domains.Domains.Project>
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Division { get; set; }
                public string KeyAccountManager { get; set; }
                public string DeliveryResponsibleName { get; set; }
                public DateTime ProjectStartDate { get; set; }
                public bool IsNotSubmitted { get; set; } = false;
            }
        }

    }
}