using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.DoDs.AddEditDoDReport
{
    public class AddEditDoDReportLinkCommand : IRequest<int>
    {
        public int ProjectId { get; set; }
        public int YearWeek { get; set; }
        public string LinkToReport { get; set; }
        public string ReportFileName { get; set; }

        public class Handler : ExecutionHandlerBase<AddEditDoDReportLinkCommand, int>
        {
            private readonly ReportDbContext _dbContext;

            public Handler(ReportDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public override async Task<int> HandleAsync(AddEditDoDReportLinkCommand request)
            {
                var dodRecords = await _dbContext.DoDReports.Where(d =>
                    d.ProjectId == request.ProjectId & d.YearWeek == request.YearWeek).ToListAsync();
                
                dodRecords.ForEach(r => r.SetReportFile(r.ReportFileName, r.LinkToReport));

                await _dbContext.SaveChangesAsync();

                return 1;
            }
        }
    }
}