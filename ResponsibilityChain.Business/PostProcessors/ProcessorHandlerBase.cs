using System.Threading.Tasks;

namespace ResponsibilityChain.Business.PostProcessors
{
    public class ProcessorHandlerBase<TRequest, TResponse> : Handler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly PreProcessor<TRequest, TResponse>[] _preProcessors;
        private readonly PostProcessor<TRequest, TResponse>[] _postProcessors;

        public ProcessorHandlerBase(PreProcessor<TRequest, TResponse>[] preProcessors,
            PostProcessor<TRequest, TResponse>[] postProcessors)
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

    public class PreProcessor<TRequest, TResponse> : Handler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public override Task HandleAsync(TRequest request)
        {
            return Task.CompletedTask;
        }
    }

    public class PostProcessor<TRequest, TResponse> : Handler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public override Task HandleAsync(TRequest request)
        {
            return Task.CompletedTask;
        }
    }
}