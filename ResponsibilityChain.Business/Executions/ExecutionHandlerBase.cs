using System.Linq;
using System.Threading.Tasks;

namespace ResponsibilityChain.Business.Executions
{
    public class ExecutionHandlerBase<TRequest, TResponse> : BranchHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
    }

}