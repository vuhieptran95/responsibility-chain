using System.Threading.Tasks;

namespace ResponsibilityChain.Business.EventsHandlers
{
    public class EventsHandler<TRequest, TResponse> : BranchHandler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        private readonly PreEvent<TRequest, TResponse>[] _preEvents;
        private readonly PostEvent<TRequest, TResponse>[] _postEvents;
        

        public EventsHandler(PreEvent<TRequest, TResponse>[] preEvents, PostEvent<TRequest, TResponse>[] postEvents)
        {
            _preEvents = preEvents;
            _postEvents = postEvents;
        }
        

        public override async Task HandleAsync(TRequest request)
        {
            foreach (var preEvent in _preEvents)
            {
                await preEvent.HandleAsync(request);
            }

            await base.HandleAsync(request);

            foreach (var postEvent in _postEvents)
            {
                await postEvent.HandleAsync(request);
            }
        }
    }

    public class PreEvent<TRequest, TResponse> : BranchHandler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        public override Task HandleAsync(TRequest request)
        {
            return Task.CompletedTask;
        }
    }

    public class PostEvent<TRequest, TResponse> : BranchHandler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        public override Task HandleAsync(TRequest request)
        {
            return Task.CompletedTask;
        }
    }
}