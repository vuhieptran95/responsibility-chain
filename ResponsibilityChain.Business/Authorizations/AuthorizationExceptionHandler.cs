using System;
using System.Threading.Tasks;

namespace ResponsibilityChain.Business.Authorizations
{
    public class AuthorizationExceptionHandler<TRequest, TResponse> : Handler<TRequest, TResponse>
    {
        public override Task<TResponse> HandleAsync(TRequest request)
        {
            throw new UnauthorizedAccessException("Wew, seems like you're unauthorized!");
        }
    }
}