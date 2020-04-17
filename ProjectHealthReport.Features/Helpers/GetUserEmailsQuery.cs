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
        public class Handler: ExecutionHandler<GetUserEmailsQuery, List<string>>
        {
            public override Task HandleAsync(GetUserEmailsQuery request)
            {
                AuthorizationHelper.ProjectManagementOffice.AddRange(AuthorizationHelper.KeyAccountManagers);
                AuthorizationHelper.ProjectManagementOffice.AddRange(AuthorizationHelper.DeliveryManagerAccounts);
                AuthorizationHelper.ProjectManagementOffice.AddRange(AuthorizationHelper.ProjectManagers);
                
                request.Response = (AuthorizationHelper.ProjectManagementOffice.Select(i => i.Item1).ToList());
                return Task.CompletedTask;
            }
        }

        public List<string> Response { get; set; }
    }
}