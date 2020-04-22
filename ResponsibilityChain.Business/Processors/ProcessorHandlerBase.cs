using System.Threading.Tasks;

namespace ResponsibilityChain.Business.Processors
{
    public class ProcessorHandlerBase<TRequest, TResponse> : Handler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {

        public ProcessorHandlerBase(IPreProcessor<TRequest, TResponse>[] preProcessors,
            IPostProcessor<TRequest, TResponse>[] postProcessors)
        {
            foreach (var preProcessor in preProcessors)
            {
                AddHandler(preProcessor);
            }

            foreach (var postProcessor in postProcessors)
            {
                AddHandler(postProcessor);
            }
        }
    }

    public interface IPreProcessor<TRequest, TResponse> : IPreHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        
    }
    
    public interface IPostProcessor<TRequest, TResponse> : IPostHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        
    }

}