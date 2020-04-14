using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ResponsibilityChain.Business.Logging
{
    public class LoggingHandler<TRequest, TResponse> : Handler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingHandler(ILogger<TRequest> logger)
        {
            _logger = logger;
        }
        public override async Task HandleAsync(TRequest request)
        {
            _logger.LogInformation($"Logging request {request.GetType()} " + "{@request}", request);
            await base.HandleAsync(request);
            _logger.LogInformation($"Logging response: {request.Response?.GetType()} " + "{@response}", request.Response);
            
        }
    }
}