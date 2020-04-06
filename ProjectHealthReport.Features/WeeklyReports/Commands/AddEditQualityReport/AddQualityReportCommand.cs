using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Features.Common.Mappings;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.WeeklyReports.Commands.AddEditQualityReport
{
    public class AddQualityReportCommand : IRequest<int>, IMapFrom<QualityReport>
    {
        public int ProjectId { get; set; }

        [Range(0, 10000, ErrorMessage = ValidationHelper.NumberMustBeNonNegative)]
        public int CriticalBugs { get; set; }

        [Range(0, 10000, ErrorMessage = ValidationHelper.NumberMustBeNonNegative)]
        public int MajorBugs { get; set; }

        [Range(0, 10000, ErrorMessage = ValidationHelper.NumberMustBeNonNegative)]
        public int MinorBugs { get; set; }

        [Range(0, 10000, ErrorMessage = ValidationHelper.NumberMustBeNonNegative)]
        public int DoneBugs { get; set; }

        [Range(0, 10000, ErrorMessage = ValidationHelper.NumberMustBeNonNegative)]
        public int ReOpenBugs { get; set; }

        public int RemainingBugs { get; set; }
        public int YearWeek { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddQualityReportCommand, QualityReport>()
                .ConstructUsing(cmd => new QualityReport(0, cmd.ProjectId, cmd.CriticalBugs, cmd.MajorBugs,
                    cmd.MinorBugs, cmd.DoneBugs, cmd.ReOpenBugs, cmd.YearWeek, null));
        }

        public class Handler : ExecutionHandlerBase<AddQualityReportCommand, int>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }


            public override async Task<int> HandleAsync(AddQualityReportCommand request)
            {
                var qualityReport = _mapper.Map<QualityReport>(request);

                await _dbContext.QualityReports.AddAsync(qualityReport);
                await _dbContext.SaveChangesAsync();

                return 1;
            }
        }
    }
}