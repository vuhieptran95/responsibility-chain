using System;
using System.Threading.Tasks;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business.Authorizations;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Common.Authorization
{
    public class IsCoo<TRequest, TResponse> : IPreAuthorizationRule<TRequest, TResponse>
        where TRequest : Request<TResponse>
    {
        public async Task HandleAsync(TRequest request)
        {
            if (request.AuthorizationIsHandled)
            {
                await Task.CompletedTask;
                return;
            }
            
            if (request.RequestContext.UserRole == AuthorizationHelper.RoleCOO)
            {
                request.AuthorizationIsHandled = true;
            }
        }
    }
}