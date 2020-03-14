using System.Threading.Tasks;

namespace ResponsibilityChain.Business.PostProcessors
{
    public class PostProcessorHandlerBase<TRequest, TResponse> : Handler<TRequest, TResponse>
    {
        public PostProcessorHandlerBase()
        {
        }
    }
}