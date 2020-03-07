using System.Threading.Tasks;

namespace ResponsibilityChain.Business.Authorizations
{
    public class AuthorizationDefaultHandler<TRequest, TResponse> : BranchHandler<TRequest, TResponse>
    {
        public override Task<TResponse> HandleAsync(TRequest request)
        {
            return Task.FromResult<TResponse>(default);
        }
    }
}