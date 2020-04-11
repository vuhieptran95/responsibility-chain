using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.WeeklyReports.Commands.AddEditStatus
{
    public class EditStatusCommand : IRequest<int>, IMapFrom<Status>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string StatusColor { get; set; }
        public string RetrospectiveFeedBack { get; set; }
        public string ProjectStatus { get; set; }
        public DateTime? MilestoneDate { get; set; }
        public string Milestone { get; set; }
        public int YearWeek { get; set; }

        public void MappingFrom(Profile profile)
        {
            profile.CreateMap<Status, EditStatusCommand>();
            profile.CreateMap<EditStatusCommand, EditStatusCommand>();
            profile.CreateMap<EditStatusCommand, Status>()
                .ConstructUsing(cmd => new Status(cmd.Id, cmd.ProjectId, cmd.StatusColor, cmd.ProjectStatus,
                    cmd.RetrospectiveFeedBack, cmd.MilestoneDate, cmd.Milestone, cmd.YearWeek, null));
        }

        public class Handler : ExecutionHandlerBase<EditStatusCommand, int>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public override async Task<int> HandleAsync(EditStatusCommand request)
            {
                var statusInDb = await _dbContext.Statuses.AsNoTracking().SingleAsync(s => s.Id == request.Id);
                var statusProxy = _mapper.Map<EditStatusCommand>(statusInDb);
                _mapper.Map(request, statusProxy);

                _dbContext.Statuses.Update(_mapper.Map<Status>(statusProxy));
                await _dbContext.SaveChangesAsync();

                return 1;
            }
        }
    }
}