using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Helpers
{
    public class GetUserEmailsQuery : IRequest<List<string>>
    {
        public class Handler: ExecutionHandlerBase<GetUserEmailsQuery, List<string>>
        {
            public override Task<List<string>> HandleAsync(GetUserEmailsQuery request)
            {
                AuthorizationHelper.ProjectManagementOffice.AddRange(AuthorizationHelper.KeyAccountManagers);
                AuthorizationHelper.ProjectManagementOffice.AddRange(AuthorizationHelper.DeliveryManagerAccounts);
                AuthorizationHelper.ProjectManagementOffice.AddRange(AuthorizationHelper.ProjectManagers);
                
                return Task.FromResult(AuthorizationHelper.ProjectManagementOffice.Select(i => i.Item1).ToList());
            }
        }
    }
}