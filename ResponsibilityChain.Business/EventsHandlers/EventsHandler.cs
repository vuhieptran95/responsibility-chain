using System.Threading.Tasks;

namespace ResponsibilityChain.Business.EventsHandlers
{
    public class EventsHandler<TRequest, TResponse> : BranchHandler<TRequest, TResponse>
    {
        private readonly PreEvent<TRequest, TResponse>[] _preEvents;
        private readonly PostEvent<TResponse, TRequest>[] _postEvents;
        private readonly DefaultBranchHandler<TRequest, TResponse> _defaultPreEvent;
        private readonly DefaultBranchHandler<TResponse, TRequest> _defaultPostEvent;
        private readonly PreEvent<TRequest, TResponse> _preEvent;
        private readonly PostEvent<TResponse, TRequest> _postEvent;

        public EventsHandler(PreEvent<TRequest, TResponse>[] preEvents, PostEvent<TResponse, TRequest>[] postEvents,
            DefaultBranchHandler<TRequest, TResponse> defaultPreEvent,
            DefaultBranchHandler<TResponse, TRequest> defaultPostEvent)
        {
            _preEvents = preEvents;
            _preEvent = preEvents[0];
            _postEvents = postEvents;
            _postEvent = postEvents[0];
            _defaultPreEvent = defaultPreEvent;
            _defaultPostEvent = defaultPostEvent;

            CreateEventBranch();
        }
        

        public override async Task<TResponse> HandleAsync(TRequest request)
        {
            await _preEvent.Branch.HandleAsync(request);

            var response = await base.HandleAsync(request);

            await _postEvent.Branch.HandleAsync(response);

            return response;
        }

        private void CreateEventBranch()
        {
            if (_preEvents.Length < 2)
            {
                _preEvent.AddBranchHandler(_defaultPreEvent);
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
                _postEvent.AddBranchHandler(_defaultPostEvent);
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

    public class PreEvent<TRequest, TResponse> : BranchHandler<TRequest, TResponse>
    {
    }

    public class PostEvent<TResponse, TRequest> : BranchHandler<TResponse, TRequest>
    {
    }
}