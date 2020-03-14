using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Authorizations;
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.EventsHandlers;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.Logging;
using ResponsibilityChain.Business.PostProcessors;
using ResponsibilityChain.Business.PrePostRequestHandlers;
using ResponsibilityChain.Business.Validations;

namespace ResponsibilityChain.Business
{
    public class RequestHandler<TRequest, TResponse> : Handler<TRequest, TResponse>
    {
        public RequestHandler(
            PreRequestHandler<TRequest, TResponse> preRequestHandler,
            LoggingHandler<TRequest, TResponse> loggingHandler,
            AuthorizationConfig<TRequest, TResponse> authorizationConfig,
            AuthorizationHandlerBase<TRequest, TResponse> authorizationHandlerBase,
            ValidationHandler<TRequest, TResponse>[] validationHandlers,
            EventsHandler<TRequest, TResponse> eventsHandler,
            PostRequestHandler<TRequest, TResponse> postRequestHandler,
            CacheHandler<TRequest, TResponse> cacheHandler,
            ExecutionHandlerBase<TRequest, TResponse> executionHandlerBase)
        {
            AddHandler(preRequestHandler);
            AddHandler(loggingHandler);
            AddHandler(authorizationConfig);
            AddHandler(authorizationHandlerBase);
            
            foreach (var handler in validationHandlers)
            {
                AddHandler(handler);
            }
            
            AddHandler(eventsHandler);
            AddHandler(postRequestHandler);
            AddHandler(cacheHandler);
            AddHandler(executionHandlerBase);
            
            // AddHandler(postProcessorHandlerBase);
        }
    }
}