using System;
using System.Threading.Tasks;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.WeeklyReports.Commands.AddEditAdditionalInfo
{
    public class RemoveAdditionalInfoCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int IssueId { get; set; }

    public class Handler : ExecutionHandlerBase<RemoveAdditionalInfoCommand, int>
    {
        private readonly ReportDbContext _dbContext;

        public Handler(ReportDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task HandleAsync(RemoveAdditionalInfoCommand request)
        {
            var aii = new AdditionalInfoIssues(request.Id, request.IssueId, MiscHelper.IssueStatusOpen, null, null);

            _dbContext.AdditionalInfoIssues.Remove(aii);
            await _dbContext.SaveChangesAsync();

            _dbContext.Issues.Remove(new Issue(request.IssueId, null, null, null, 0, null));
            await _dbContext.SaveChangesAsync();

            // await transaction.CommitAsync();

            request.Response = 1;
        }
    }

    public int Response { get; set; }
    }
    }
