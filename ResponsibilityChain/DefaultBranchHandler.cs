using System.Threading.Tasks;

namespace ResponsibilityChain
{
    public class DefaultBranchHandler<TRequest, TResponse> : Handler<TRequest, TResponse>
    {
        public override Task<TResponse> HandleAsync(TRequest request)
        {
            return Task.FromResult<TResponse>(default);
        }
    }
}