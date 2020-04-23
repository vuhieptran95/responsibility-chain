using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Helpers
{
    public class GetDivisionNamesQuery : Request<List<string>>
    {
        public class Handler: IExecution<GetDivisionNamesQuery, List<string>>
        {
            public Task HandleAsync(GetDivisionNamesQuery request)
            {
                request.Response = (AuthorizationHelper.DeliveryManagers.Select(i => i.Value).ToList());
                return Task.CompletedTask;
            }
        }

    }
}