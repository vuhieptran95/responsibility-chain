using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResponsibilityChain.TestConsole
{
    public interface IHandler
    {
    }

    public interface IHandler<TRequest, TResponse> : IHandler where TRequest : IRequest<TResponse>
    {
        Task HandlerAsync(TRequest request, IHandler<TRequest, TResponse> next);
    }


    public class Composer<TRequest, TResponse> : IHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private List<IHandler<TRequest, TResponse>> _handlers;

        public async Task HandlerAsync(TRequest request)
        {
            // run here
            foreach (var handler in _handlers)
            {
                await handler.HandlerAsync(request, null);
            }
            // run here
        }

        public void AddHandler(IHandler<TRequest, TResponse> handler)
        {
            if (_handlers == null)
            {
                _handlers = new List<IHandler<TRequest, TResponse>>();
            }

            _handlers.Add(handler);
        }

        public Task HandlerAsync(TRequest request, IHandler<TRequest, TResponse> next)
        {
            var handler = _handlers[0];
            handler.HandlerAsync(request, _handlers[1]);
            return Task.CompletedTask;
        }
    }

    public class RequestHandlerNew<TRequest, TResponse> : Composer<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public RequestHandlerNew()
        {
        }
    }

    public class RequestA : IRequest<string>
    {
        public string Name { get; set; }
        public string Response { get; set; }
    }

   
}