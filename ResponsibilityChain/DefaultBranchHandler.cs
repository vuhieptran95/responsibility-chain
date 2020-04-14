using System.Threading.Tasks;

namespace ResponsibilityChain
{
    public class DefaultBranchHandler<TRequest, TResponse> : Handler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        public override Task HandleAsync(TRequest request)
        {
            return Task.CompletedTask;
        }
    }
}