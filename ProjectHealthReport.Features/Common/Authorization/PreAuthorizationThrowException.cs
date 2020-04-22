using System;
using System.Threading.Tasks;
using ResponsibilityChain;
using ResponsibilityChain.Business.Authorizations;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Common.Authorization
{
    public class
        PreAuthorizationThrowException<TRequest, TResponse> : IPreHandler<TRequest, TResponse>
        where TRequest : Request<TResponse>
    {
        public Task HandleAsync(TRequest request)
        {
            if (request.AuthorizationIsHandled)
            {
                return Task.CompletedTask;
            }
            
            throw new UnauthorizedAccessException("Wew, seems like you're unauthorized!");
        }
    }
}