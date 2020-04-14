using System.Threading.Tasks;

namespace ResponsibilityChain.Business.EventsHandlers
{
    public class EventsHandler<TRequest, TResponse> : BranchHandler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        private readonly PreEvent<TRequest, TResponse>[] _preEvents;
        private readonly PostEvent<TRequest, TResponse>[] _postEvents;
        private readonly DefaultBranchHandler<TRequest, TResponse> _defaultEvent;
        private readonly PreEvent<TRequest, TResponse> _preEvent;
        private readonly PostEvent<TRequest, TResponse> _postEvent;

        public EventsHandler(PreEvent<TRequest, TResponse>[] preEvents, PostEvent<TRequest, TResponse>[] postEvents,
            DefaultBranchHandler<TRequest, TResponse> defaultEvent)
        {
            _preEvents = preEvents;
            _preEvent = preEvents[0];
            _postEvents = postEvents;
            _postEvent = postEvents[0];
            _defaultEvent = defaultEvent;

            CreateEventBranch();
        }
        

        public override async Task HandleAsync(TRequest request)
        {
            await _preEvent.Branch.HandleAsync(request);

            await base.HandleAsync(request);

            await _postEvent.Branch.HandleAsync(request);
        }

        private void CreateEventBranch()
        {
            if (_preEvents.Length < 2)
            {
                _preEvent.AddBranchHandler(_defaultEvent);
            }
            else
            {
                for (int i = 1; i < _preEvents.Length; i++)
                {
                    _preEvent.AddBranchHandler(_preEvents[i]);
                }
            }

            if (_postEvents.Length < 2)
            {
                _postEvent.AddBranchHandler(_defaultEvent);
            }
            else
            {
                for (int i = 1; i < _postEvents.Length; i++)
                {
                    _postEvent.AddBranchHandler(_postEvents[i]);
                }
            }
        }
    }

    public class PreEvent<TRequest, TResponse> : BranchHandler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
    }

    public class PostEvent<TRequest, TResponse> : BranchHandler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
    }
}