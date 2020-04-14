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

namespace ProjectHealthReport.Features.WeeklyReports.Commands.AddEditQualityReport
{
    public class EditQualityReportCommand : IRequest<int>, IMapFrom<QualityReport>
    {
        public int Id { get; set; }
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

        public void MappingFrom(Profile profile)
        {
            profile.CreateMap<QualityReport, EditQualityReportCommand>();
            profile.CreateMap<EditQualityReportCommand, EditQualityReportCommand>();
            profile.CreateMap<EditQualityReportCommand, QualityReport>()
                .ConstructUsing(cmd => new QualityReport(cmd.Id, cmd.ProjectId, cmd.CriticalBugs, cmd.MajorBugs,
                    cmd.MinorBugs, cmd.DoneBugs, cmd.ReOpenBugs, cmd.YearWeek, null));
        }

        public class Handler : ExecutionHandlerBase<EditQualityReportCommand, int>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public override async Task HandleAsync(EditQualityReportCommand request)
            {
                var reportInDb = await _dbContext.QualityReports.AsNoTracking().SingleAsync(q => q.Id == request.Id);
                var reportProxy = _mapper.Map<EditQualityReportCommand>(reportInDb);
                _mapper.Map(request, reportProxy);

                _dbContext.Update(_mapper.Map<QualityReport>(reportProxy));
                await _dbContext.SaveChangesAsync();

                request.Response = 1;
            }
        }

        public int Response { get; set; }
    }
}