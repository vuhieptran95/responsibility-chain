using System.Threading.Tasks;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business.Authorizations;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Common.Authorization
{
    public class CommonAuthorization<TRequest, TResponse> : AuthorizationHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public CommonAuthorization(IsCoo<TRequest, TResponse> isCoo, IsPmo<TRequest, TResponse> isPmo)
        {
            Add(isCoo);
            Add(isPmo);
        }    
    }
    
    public class IsCoo<TRequest, TResponse> : AuthorizationHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly RequestContext _requestContext;

        public IsCoo(RequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        public override async Task HandleAsync(TRequest request)
        {
            if (_requestContext.UserRole == AuthorizationHelper.RoleCOO)
            {
                await HandleBranchAsync(request);
                return;
            }

            await base.HandleAsync(request);
        }
    }
    
    public class IsPmo<TRequest, TResponse> : AuthorizationHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly RequestContext _requestContext;

        public IsPmo(RequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        public override async Task HandleAsync(TRequest request)
        {
            if (_requestContext.UserRole == AuthorizationHelper.RolePMOAssistant)
            {
                await HandleBranchAsync(request);
                return;
            }

            await base.HandleAsync(request);
        }
    }
}