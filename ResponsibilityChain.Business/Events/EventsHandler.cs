using System.Threading.Tasks;

namespace ResponsibilityChain.Business.Events
{
    public class EventsHandler<TRequest, TResponse> : Handler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public EventsHandler(IPreEvent<TRequest, TResponse>[] preEvents, IPostEvent<TRequest, TResponse>[] postEvents)
        {
            foreach (var preEvent in preEvents)
            {
                AddHandler(preEvent);
            }

            foreach (var postEvent in postEvents)
            {
                AddHandler(postEvent);
            }
        }
    }

    public interface IPreEvent<TRequest, TResponse> : IPreHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
    }
    
    public interface IPostEvent<TRequest, TResponse> : IPostHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
    }
}