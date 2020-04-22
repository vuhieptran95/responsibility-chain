using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ResponsibilityChain.Business.RequestContexts;

namespace ResponsibilityChain.Business.Logging
{
    public class LoggingHandler<TRequest, TResponse> : Handler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly RequestContext _requestContext;

        public LoggingHandler(ILogger<TRequest> logger, RequestContext requestContext)
        {
            _logger = logger;
            _requestContext = requestContext;
        }
        public override async Task HandleAsync(TRequest request)
        {
            if (request is IRequiredRequestContext req)
            {
                req.RequestContext = _requestContext;
            }
            
            _logger.LogInformation($"Logging request {request.GetType()} " + "{@request}", request);
            await base.HandleAsync(request);
            _logger.LogInformation($"Logging response: {request.Response?.GetType()} " + "{@response}", request.Response);
            
        }
    }
}