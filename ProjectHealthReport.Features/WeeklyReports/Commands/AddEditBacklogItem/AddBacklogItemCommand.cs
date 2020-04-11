using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Mappings;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.WeeklyReports.Commands.AddEditBacklogItem
{
    public class AddBacklogItemCommand : IRequest<int>, IMapFrom<BacklogItem>
    {
        public int ProjectId { get; set; }

        [Range(0, 10000, ErrorMessage = ValidationHelper.NumberMustBeNonNegative)]
        public int? Sprint { get; set; }

        [Range(0, 10000, ErrorMessage = ValidationHelper.NumberMustBeNonNegative)]
        public int ItemsAdded { get; set; }

        [Range(0, 10000, ErrorMessage = ValidationHelper.NumberMustBeNonNegative)]
        public int? StoryPointsAdded { get; set; }

        [Range(0, 10000, ErrorMessage = ValidationHelper.NumberMustBeNonNegative)]
        public int ItemsDone { get; set; }

        [Range(0, 10000, ErrorMessage = ValidationHelper.NumberMustBeNonNegative)]
        public int? StoryPointsDone { get; set; }

        public int YearWeek { get; set; }
        public void MappingFrom(Profile profile)
        {
            profile.CreateMap<AddBacklogItemCommand, BacklogItem>()
                .ConstructUsing(cmd => new BacklogItem(0, cmd.ProjectId, cmd.Sprint, cmd.ItemsAdded,
                    cmd.StoryPointsAdded, cmd.ItemsDone, cmd.StoryPointsDone,
                    cmd.YearWeek, null));
        }

        public class Handler : ExecutionHandlerBase<AddBacklogItemCommand, int>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public override async Task<int> HandleAsync(AddBacklogItemCommand request)
            {
                var backlogItem = _mapper.Map<BacklogItem>(request);

                await _dbContext.BacklogItems.AddAsync(backlogItem);
                await _dbContext.SaveChangesAsync();

                return 1;
            }
        }
    }
}