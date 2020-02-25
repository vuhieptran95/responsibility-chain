using ResponsibilityChain.Business.Authorizations;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.Validations;

namespace ResponsibilityChain.Business
{
    public class RequestHandler<TRequest, TResponse> : Handler<TRequest, TResponse>
    {
        public RequestHandler(
            AuthorizationHandler<TRequest, TResponse> authorizationHandlerBase,
            ValidationHandler<TRequest, TResponse>[] validationHandlers,
            ExecutionHandler<TRequest, TResponse> executionHandlerBase)
        {
            AddHandler(authorizationHandlerBase);
            foreach (var handler in validationHandlers)
            {
                AddHandler(handler);
            }
            AddHandler(executionHandlerBase);
        }
    }
}