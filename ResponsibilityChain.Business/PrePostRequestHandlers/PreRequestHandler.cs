using System.Threading.Tasks;
using ResponsibilityChain.Business.RequestContexts;

namespace ResponsibilityChain.Business.PrePostRequestHandlers
{
    public class PreRequestHandler<TRequest, TResponse>: Handler<TRequest, TResponse>
    {
        private readonly RequestContext _requestContext;

        public PreRequestHandler(RequestContext requestContext)
        {
            _requestContext = requestContext;
        }
        public override Task<TResponse> HandleAsync(TRequest request)
        {
            _requestContext.Request = request;
            return base.HandleAsync(request);
        }
    }
}