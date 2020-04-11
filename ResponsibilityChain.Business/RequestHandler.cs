﻿using ResponsibilityChain.Business.AuthorizationConfigs;
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
            LoggingHandler<TRequest, TResponse> loggingHandler,
            AuthorizationConfig<TRequest, TResponse> authorizationConfig,
            AuthorizationHandlerBase<TRequest, TResponse> authorizationHandlerBase,
            ValidationHandler<TRequest, TResponse>[] validationHandlers,
            EventsHandler<TRequest, TResponse> eventsHandler,
            CacheHandler<TRequest, TResponse> cacheHandler,
            PostRequestHandler<TRequest, TResponse> postRequestHandler,
            ExecutionHandlerBase<TRequest, TResponse> executionHandlerBase)
        {
            AddHandler(loggingHandler);
            AddHandler(authorizationConfig);
            AddHandler(authorizationHandlerBase);
            
            foreach (var handler in validationHandlers)
            {
                AddHandler(handler);
            }
            
            AddHandler(eventsHandler);
            AddHandler(cacheHandler);
            AddHandler(postRequestHandler);
            AddHandler(executionHandlerBase);
            
            // AddHandler(postProcessorHandlerBase);
        }
    }
}