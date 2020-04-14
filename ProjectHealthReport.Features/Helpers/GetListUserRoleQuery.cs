using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Helpers
{
    public class GetListUserRoleQuery : IRequest<List<(string, string)>>
    {
        public class Handler: ExecutionHandlerBase<GetListUserRoleQuery, List<(string,string)>>
        {
            public override Task HandleAsync(GetListUserRoleQuery request)
            {
                request.Response = (AuthorizationHelper.UserRoleList);
                return Task.CompletedTask;
            }
        }

        public List<(string, string)> Response { get; set; }
    }
}