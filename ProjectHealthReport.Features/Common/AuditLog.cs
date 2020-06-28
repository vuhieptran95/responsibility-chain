using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Features.Projects.Commands;
using ProjectHealthReport.Features.WeeklyReports.Commands.AddEditWeeklyReportPhr;
using ResponsibilityChain.Business.Auditing;

namespace ProjectHealthReport.Features.Common
{
    public class AuditLog : IPostAudit<AddEditWeeklyReportPhrCommand, int>, IPostAudit<EditProjectNonMasterDataCommand, int>
    {
        private readonly ReportDbContext _dbContext;

        public AuditLog(ReportDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task HandleAsync(AddEditWeeklyReportPhrCommand request)
        {
            await InsertLog(new Domains.Domains.AuditLog()
            {
                CommandText = JsonConvert.SerializeObject(request),
                CommandType = typeof(AddEditWeeklyReportPhrCommand).FullName,
                Recorded = DateTime.Now,
                EntityId = "Project/" + request.Report.ProjectId,
                Role = request.RequestContext.UserRole,
                User = request.RequestContext.UserEmail
            });
        }

        public async Task HandleAsync(EditProjectNonMasterDataCommand request)
        {
            await InsertLog(new Domains.Domains.AuditLog()
            {
                CommandText = JsonConvert.SerializeObject(request),
                CommandType = typeof(EditProjectNonMasterDataCommand).FullName,
                Recorded = DateTime.Now,
                EntityId = "Project/" + request.Id,
                Role = request.RequestContext.UserRole,
                User = request.RequestContext.UserEmail
            });
        }

        private async Task InsertLog(Domains.Domains.AuditLog log)
        {
            await _dbContext.AuditLogs.AddAsync(log);

            await _dbContext.SaveChangesAsync();
        }
    }
}