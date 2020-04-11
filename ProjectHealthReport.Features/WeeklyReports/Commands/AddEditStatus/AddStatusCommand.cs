using System;
using System.Threading.Tasks;
using AutoMapper;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.WeeklyReports.Commands.AddEditStatus
{
    public class AddStatusCommand : IRequest<int>, IMapFrom<Status>
    {
        public int ProjectId { get; set; }
        public string StatusColor { get; set; }
        public string RetrospectiveFeedBack { get; set; }
        public string ProjectStatus { get; set; }
        public DateTime? MilestoneDate { get; set; }
        public string Milestone { get; set; }
        public int YearWeek { get; set; }

        public void MappingFrom(Profile profile)
        {
            profile.CreateMap<AddStatusCommand, Status>()
                .ConstructUsing(cmd => new Status(0, cmd.ProjectId, cmd.StatusColor, cmd.ProjectStatus,
                    cmd.RetrospectiveFeedBack, cmd.MilestoneDate, cmd.Milestone, cmd.YearWeek, null));
        }

        public class AddStatusCommandHandler : ExecutionHandlerBase<AddStatusCommand, int>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public AddStatusCommandHandler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public override async Task<int> HandleAsync(AddStatusCommand request)
            {
                var status = _mapper.Map<Status>(request);

                await _dbContext.Statuses.AddAsync(status);
                await _dbContext.SaveChangesAsync();

                return 1;
            }
        }
    }
}