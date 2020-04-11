using System.Threading.Tasks;
using ResponsibilityChain.Business.RequestContexts;

namespace ResponsibilityChain.Business.PrePostRequestHandlers
{
    public class PostRequestHandler<TRequest, TResponse>: Handler<TRequest, TResponse>
    {
        private readonly RequestContext _requestContext;

        public PostRequestHandler(RequestContext requestContext)
        {
            _requestContext = requestContext;
        }
        public override async Task<TResponse> HandleAsync(TRequest request)
        {
            var response = await base.HandleAsync(request);
            _requestContext.TempData.Add(request.GetType().FullName, response);
            
            return response;
        }
    }
}