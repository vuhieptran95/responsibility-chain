using System.Linq;

namespace ResponsibilityChain.Business.Authorizations
{
    public class AuthorizationHandlerBase<TRequest, TResponse> : BranchHandler<TRequest, TResponse>
    {
        public AuthorizationHandlerBase(AuthorizationHandler<TRequest, TResponse> [] authorizationHandlers,
            AuthorizationExceptionHandler<TRequest, TResponse> authorizationExceptionHandler)
        {
            if (authorizationHandlers.Length > 1)
            {
                var handlers = authorizationHandlers.Skip(1);
                foreach (var handler in authorizationHandlers)
                {
                    AddBranchHandler(handler);
                }
                AddBranchHandler(authorizationExceptionHandler);
            }
        }
    }
}