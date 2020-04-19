using System;
using System.Threading.Tasks;

namespace ResponsibilityChain.Business.Authorizations
{
    public class AuthorizationExceptionHandler<TRequest, TResponse> : Handler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        public override Task HandleAsync(TRequest request)
        {
            throw new UnauthorizedAccessException("Wew, seems like you're unauthorized!");
        }
    }
}