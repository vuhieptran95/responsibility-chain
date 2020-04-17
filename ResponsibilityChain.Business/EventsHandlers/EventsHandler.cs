using System.Threading.Tasks;

namespace ResponsibilityChain.Business.EventsHandlers
{
    public class EventsHandler<TRequest, TResponse> : BranchHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IPreEvent<TRequest, TResponse>[] _preEvents;
        private readonly IPostEvent<TRequest, TResponse>[] _postEvents;


        public EventsHandler(IPreEvent<TRequest, TResponse>[] preEvents, IPostEvent<TRequest, TResponse>[] postEvents)
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

    public interface IPreEvent<TRequest, TResponse> : IHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
    }
    
    public interface IPostEvent<TRequest, TResponse> : IHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
    }
}