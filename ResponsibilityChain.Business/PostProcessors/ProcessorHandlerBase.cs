using System.Threading.Tasks;

namespace ResponsibilityChain.Business.PostProcessors
{
    public class ProcessorHandlerBase<TRequest, TResponse> : Handler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IPreProcessor<TRequest, TResponse>[] _preProcessors;
        private readonly IPostProcessor<TRequest, TResponse>[] _postProcessors;

        public ProcessorHandlerBase(IPreProcessor<TRequest, TResponse>[] preProcessors,
            IPostProcessor<TRequest, TResponse>[] postProcessors)
        {
            _preProcessors = preProcessors;
            _postProcessors = postProcessors;
        }

        public override async Task HandleAsync(TRequest request)
        {
            foreach (var preProcessor in _preProcessors)
            {
                await preProcessor.HandleAsync(request);
            }

            await base.HandleAsync(request);

            foreach (var postProcessor in _postProcessors)
            {
                await postProcessor.HandleAsync(request);
            }
        }
    }

    public interface IPreProcessor<TRequest, TResponse> : IHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        
    }
    
    public interface IPostProcessor<TRequest, TResponse> : IHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        
    }

}