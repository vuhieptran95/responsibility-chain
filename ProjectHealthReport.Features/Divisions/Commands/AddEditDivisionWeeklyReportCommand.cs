using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
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

        public class Dto : IMapFrom<DivisionProjectStatus>
        {
            public int StatusId { get; set; }
            public int ProjectId { get; set; }
            public string ProjectName { get; set; }
            [Required] public string StatusColor { get; set; }
            public string ProjectStatus { get; set; }
            public string Actions { get; set; }
            public int YearWeek { get; set; }

            public void MappingFrom(Profile profile)
            {
                profile.CreateMap<Dto, DivisionProjectStatus>();
            }
        }
        
        public class Handler: ExecutionHandlerBase<AddEditDivisionWeeklyReportCommand, int>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public override async Task<int> HandleAsync(AddEditDivisionWeeklyReportCommand request)
            {
                var statuses = _mapper.Map<IEnumerable<DivisionProjectStatus>>(request.DivisionProjectStatuses);
                foreach (var status in statuses)
                {
                    _ = status.Id == 0
                        ? await _dbContext.DivisionProjectStatuses.AddAsync(status)
                        :  _dbContext.DivisionProjectStatuses.Update(status);
                }

                await _dbContext.SaveChangesAsync();

                return 1;
            }
        }
    }
}