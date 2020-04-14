using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Mappings;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.WeeklyReports.Commands.AddEditAdditionalInfo
{
    public class AddAdditionalInfoCommand : IRequest<int>, IMapFrom<Issue>
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public int ProjectId { get; set; }
        public string Item { get; set; }
        public string Impact { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        public int YearWeek { get; set; }

        public void MappingFrom(Profile profile)
        {
            profile.CreateMap<AddAdditionalInfoCommand, Issue>()
                .ConstructUsing(cmd => new Issue(0, cmd.Item, cmd.Impact, cmd.Action,
                    cmd.YearWeek, null));
        }

        public class Handler : ExecutionHandlerBase<AddAdditionalInfoCommand, int>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public override async Task HandleAsync(AddAdditionalInfoCommand request)
            {
                if (request.IssueId == 0)
                {
                    var issue = _mapper.Map<Issue>(request);

                    await _dbContext.Issues.AddAsync(issue);
                    await _dbContext.SaveChangesAsync();

                    request.IssueId = issue.Id;
                }

                var additionalInfoInDb = await _dbContext.AdditionalInfos.SingleOrDefaultAsync(a =>
                    a.YearWeek == request.YearWeek && a.ProjectId == request.ProjectId);

                if (additionalInfoInDb != null)
                {
                    var aii = new AdditionalInfoIssues(additionalInfoInDb.Id, request.IssueId, request.Status, null,
                        null);

                    await _dbContext.AdditionalInfoIssues.AddAsync(aii);
                    await _dbContext.SaveChangesAsync();

                    request.Id = additionalInfoInDb.Id;
                }
                else
                {
                    var additionalInfo = new AdditionalInfo(0, request.ProjectId, request.YearWeek, null, null);

                    await _dbContext.AdditionalInfos.AddAsync(additionalInfo);
                    await _dbContext.SaveChangesAsync();

                    request.Id = additionalInfo.Id;

                    var aii = new AdditionalInfoIssues(additionalInfo.Id, request.IssueId, request.Status, null, null);

                    await _dbContext.AdditionalInfoIssues.AddAsync(aii);
                    await _dbContext.SaveChangesAsync();
                }

                request.Response = 1;
            }
        }

        public int Response { get; set; }
    }
}