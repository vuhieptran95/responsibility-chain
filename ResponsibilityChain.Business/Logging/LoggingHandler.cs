using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ResponsibilityChain.Business.Logging
{
    public class LoggingHandler<TRequest, TResponse> : Handler<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingHandler(ILogger<TRequest> logger)
        {
            _logger = logger;
        }
        public override async Task<TResponse> HandleAsync(TRequest request)
        {
            _logger.LogInformation($"Logging request {request.GetType()} " + "{@request}", request);
            var result=  await base.HandleAsync(request);
            _logger.LogInformation($"Logging response: {result?.GetType()} " + "{@response}", result);
            return result;
        }
    }
}