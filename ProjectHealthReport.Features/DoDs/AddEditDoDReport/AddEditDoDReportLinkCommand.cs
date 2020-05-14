using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.DoDs.AddEditDoDReport
{
    public class AddEditDoDReportLinkCommand : Request<int>
    {
        public int ProjectId { get; set; }
        public int YearWeek { get; set; }
        public string LinkToReport { get; set; }
        public string ReportFileName { get; set; }
        
        public class AuthorizationConfig: IAuthorizationConfig<AddEditDoDReportLinkCommand>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new []{Resources.DoDReport}, new []{Actions.Read, Actions.Create, Actions.Update}),
                    (new []{Resources.Project}, new []{Actions.Read})
                };
            }
        }

        public class Handler : IExecution<AddEditDoDReportLinkCommand, int>
        {
            private readonly ReportDbContext _dbContext;

            public Handler(ReportDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task HandleAsync(AddEditDoDReportLinkCommand request)
            {
                var dodRecords = await _dbContext.DoDReports.Where(d =>
                    d.ProjectId == request.ProjectId & d.YearWeek == request.YearWeek).ToListAsync();
                
                dodRecords.ForEach(r => r.SetReportFile(request.ReportFileName, request.LinkToReport));

                await _dbContext.SaveChangesAsync();

                request.Response = 1;
            }
        }

    }
}