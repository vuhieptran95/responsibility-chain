using System.Linq;
using System.Threading.Tasks;

namespace ResponsibilityChain.Business.Authorizations
{
    public class AuthorizationHandlerBase<TRequest, TResponse> : Handler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public AuthorizationHandlerBase(
            AuthorizationHandler<TRequest, TResponse> authorizationHandler,
            AuthorizationExceptionHandler<TRequest, TResponse> authorizationExceptionHandler)
        {
            AddBranch(authorizationHandler);
            AddBranch(authorizationExceptionHandler);
        }

        public override async Task HandleAsync(TRequest request)
        {
            await HandleBranchAsync(request);
            
            await base.HandleAsync(request);
        }
    }
}