using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Helpers
{
    public class GetDivisionNamesQuery : IRequest<List<string>>
    {
        public class Handler: ExecutionHandler<GetDivisionNamesQuery, List<string>>
        {
            public override Task HandleAsync(GetDivisionNamesQuery request)
            {
                request.Response = (AuthorizationHelper.DeliveryManagers.Select(i => i.Value).ToList());
                return Task.CompletedTask;
            }
        }

        public List<string> Response { get; set; }
    }
}