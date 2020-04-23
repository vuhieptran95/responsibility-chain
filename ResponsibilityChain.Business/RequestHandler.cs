using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Authorizations;
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.Events;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.Logging;
using ResponsibilityChain.Business.Processors;
using ResponsibilityChain.Business.RequestContexts;
using ResponsibilityChain.Business.Validations;

namespace ResponsibilityChain.Business
{
    public class RequestHandler<TRequest, TResponse> : Handler<TRequest, TResponse> where TRequest: Request<TResponse>
    {
        public RequestHandler(
            LoggingHandler<TRequest, TResponse> loggingHandler,
            AuthorizationConfigBase<TRequest, TResponse> authorizationConfigBase,
            AuthorizationHandler<TRequest, TResponse> authorizationHandler,
            ValidationHandlerBase<TRequest, TResponse> validationHandlerBase,
            EventsHandler<TRequest, TResponse> eventsHandler,
            CacheHandler<TRequest, TResponse> cacheHandler,
            ProcessorHandlerBase<TRequest, TResponse> processorHandlerBase,
            ExecutionHandlerBase<TRequest, TResponse> executionHandlerBase)
        {
            AddHandler(loggingHandler);
            AddHandler(authorizationConfigBase);
            AddHandler(authorizationHandler);
            AddHandler(validationHandlerBase);
            AddHandler(eventsHandler);
            AddHandler(cacheHandler);
            AddHandler(processorHandlerBase);
            AddHandler(executionHandlerBase);
        }
    }
}