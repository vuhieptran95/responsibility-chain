﻿using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Authorizations;
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.EventsHandlers;
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
            AuthorizationConfig<TRequest, TResponse> authorizationConfig,
            AuthorizationHandlerBase<TRequest, TResponse> authorizationHandlerBase,
            ValidationHandlerBase<TRequest, TResponse> validationHandlerBase,
            EventsHandler<TRequest, TResponse> eventsHandler,
            CacheHandler<TRequest, TResponse> cacheHandler,
            ProcessorHandlerBase<TRequest, TResponse> processorHandlerBase,
            ExecutionHandler<TRequest, TResponse> executionHandler)
        {
            AddHandler(loggingHandler);
            AddHandler(authorizationConfig);
            AddHandler(authorizationHandlerBase);
            AddHandler(validationHandlerBase);
            AddHandler(eventsHandler);
            AddHandler(cacheHandler);
            AddHandler(processorHandlerBase);
            AddHandler(executionHandler);
        }
    }
}