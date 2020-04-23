using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Helpers
{
    public class GetUserEmailsQuery : Request<List<string>>
    {
        public class Handler: IExecution<GetUserEmailsQuery, List<string>>
        {
            public Task HandleAsync(GetUserEmailsQuery request)
            {
                AuthorizationHelper.ProjectManagementOffice.AddRange(AuthorizationHelper.KeyAccountManagers);
                AuthorizationHelper.ProjectManagementOffice.AddRange(AuthorizationHelper.DeliveryManagerAccounts);
                AuthorizationHelper.ProjectManagementOffice.AddRange(AuthorizationHelper.ProjectManagers);
                
                request.Response = (AuthorizationHelper.ProjectManagementOffice.Select(i => i.Item1).ToList());
                return Task.CompletedTask;
            }
        }

    }
}