using System.Threading.Tasks;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Features.Projects.Queries.GetProjects;
using ResponsibilityChain;
using ResponsibilityChain.Business.Authorizations;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Common.Authorization
{
    public class IsPmo<TRequest, TResponse> : IPreAuthorizationRule<TRequest, TResponse>
        where TRequest : Request<TResponse>
    {
        public async Task HandleAsync(TRequest request)
        {
            if (request.AuthorizationIsHandled)
            {
                await Task.CompletedTask;
                return;
            }
            
            if (request.RequestContext.UserRole == AuthorizationHelper.RolePMOAssistant)
            {
                request.AuthorizationIsHandled = true;
            }
        }
    }
}