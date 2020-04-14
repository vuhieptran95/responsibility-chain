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
    public class EditAdditionalInfoCommand : IRequest<int>, IMapFrom<Issue>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int IssueId { get; set; }
        public string Item { get; set; }
        public string Impact { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        public int OpenedYearWeek { get; set; }
        public int YearWeek { get; set; }
        
        public void MappingFrom(Profile profile)
        {
            profile.CreateMap<EditAdditionalInfoCommand, EditAdditionalInfoCommand>();
            profile.CreateMap<Issue, EditAdditionalInfoCommand>()
                .ForMember(des => des.IssueId, opt => opt.MapFrom(src => src.Id));
            profile.CreateMap<EditAdditionalInfoCommand, Issue>()
                .ConstructUsing(cmd =>
                    new Issue(cmd.IssueId, cmd.Item, cmd.Impact, cmd.Action, cmd.OpenedYearWeek, null));
        }
        

        public class Handler : ExecutionHandlerBase<EditAdditionalInfoCommand, int>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public override async Task HandleAsync(EditAdditionalInfoCommand request)
            {
                var aiiInDb = await _dbContext.AdditionalInfoIssues.SingleAsync(aii =>
                    aii.IssueId == request.IssueId && aii.AdditionalInfo.YearWeek == request.YearWeek &&
                    aii.AdditionalInfo.ProjectId == request.ProjectId);
                
                aiiInDb.SetStatus(request.Status);

                await _dbContext.SaveChangesAsync();

                if (request.YearWeek == request.OpenedYearWeek)
                {
                    var issue = await _dbContext.Issues.AsNoTracking().SingleAsync(i => i.Id == request.IssueId);
                    var issueProxy = _mapper.Map<EditAdditionalInfoCommand>(issue);
                    
                    _mapper.Map(request, issueProxy);

                    _dbContext.Update(_mapper.Map<Issue>(issueProxy));
                    await _dbContext.SaveChangesAsync();
                }

                request.Response = 1;
            }
        }

        public int Response { get; set; }
    }
}