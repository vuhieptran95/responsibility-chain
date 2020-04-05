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
        public class Handler: ExecutionHandlerBase<GetDivisionNamesQuery, List<string>>
        {
            public override Task<List<string>> HandleAsync(GetDivisionNamesQuery request)
            {
                return Task.FromResult(AuthorizationHelper.DeliveryManagers.Select(i => i.Value).ToList());
            }
        }
    }
}