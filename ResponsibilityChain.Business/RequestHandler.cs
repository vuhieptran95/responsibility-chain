using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Authorizations;
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.Events;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.Logging;
using ResponsibilityChain.Business.PostProcessors;
using ResponsibilityChain.Business.Validations;

namespace ResponsibilityChain.Business
{
    public class RequestHandler<TRequest, TResponse> : Handler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        public RequestHandler(
            LoggingHandler<TRequest, TResponse> loggingHandler,
            AuthorizationConfigBase<TRequest, TResponse> authorizationConfigBase,
            AuthorizationHandlerBase<TRequest, TResponse> authorizationHandlerBase,
            ValidationHandlerBase<TRequest, TResponse> validationHandlerBase,
            EventsHandler<TRequest, TResponse> eventsHandler,
            CacheHandler<TRequest, TResponse> cacheHandler,
            ProcessorHandlerBase<TRequest, TResponse> processorHandlerBase,
            ExecutionHandlerBase<TRequest, TResponse> executionHandlerBase)
        {
            Add(loggingHandler);
            Add(authorizationConfigBase);
            Add(authorizationHandlerBase);
            Add(validationHandlerBase);
            Add(eventsHandler);
            Add(cacheHandler);
            Add(processorHandlerBase);
            Add(executionHandlerBase);
        }
    }
}