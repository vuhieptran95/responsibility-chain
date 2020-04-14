using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Mappings;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.WeeklyReports.Commands.AddEditBacklogItem
{
    public class EditBacklogItemCommand : IRequest<int>, IMapFrom<BacklogItem>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = ValidationHelper.NumberMustBeNonNegative)]
        public int? Sprint { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = ValidationHelper.NumberMustBeNonNegative)]
        public int ItemsAdded { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = ValidationHelper.NumberMustBeNonNegative)]
        public int? StoryPointsAdded { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = ValidationHelper.NumberMustBeNonNegative)]
        public int ItemsDone { get; set; }

        [Range(0, Int32.MaxValue, ErrorMessage = ValidationHelper.NumberMustBeNonNegative)]
        public int? StoryPointsDone { get; set; }

        public int YearWeek { get; set; }
        
        public void MappingFrom(Profile profile)
        {
            profile.CreateMap<BacklogItem, EditBacklogItemCommand>();
            profile.CreateMap<EditBacklogItemCommand, EditBacklogItemCommand>();
            profile.CreateMap<EditBacklogItemCommand, BacklogItem>()
                .ConstructUsing(cmd => new BacklogItem(cmd.Id, cmd.ProjectId, cmd.Sprint, cmd.ItemsAdded,
                    cmd.StoryPointsAdded, cmd.ItemsDone, cmd.StoryPointsDone,
                    cmd.YearWeek, null));
        }

        public class Handler : ExecutionHandlerBase<EditBacklogItemCommand, int>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public override async Task HandleAsync(EditBacklogItemCommand request)
            {
                var backlogItemInDb = await _dbContext.BacklogItems.AsNoTracking().SingleAsync(b => b.Id == request.Id);
                var backlogItemProxy = _mapper.Map<EditBacklogItemCommand>(backlogItemInDb);
                
                _mapper.Map(request, backlogItemProxy);

                _dbContext.BacklogItems.Update(_mapper.Map<BacklogItem>(backlogItemProxy));

                await _dbContext.SaveChangesAsync();

                request.Response = 1;
            }
        }

        public int Response { get; set; }
    }
}