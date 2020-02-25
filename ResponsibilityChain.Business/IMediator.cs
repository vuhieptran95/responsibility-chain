using System.Threading.Tasks;
using Autofac;

namespace ResponsibilityChain.Business
{
    public interface IMediator
    {
        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request);
    }

    public class Mediator: IMediator
    {
        private readonly CustomScope _customScope;

        public Mediator(CustomScope customScope)
        {
            _customScope = customScope;
        }

        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            var requestType = request.GetType();
            
            var handlerType = typeof(RequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
            
            var requestHandler = _customScope.Scope.Resolve(handlerType);

            return (Task<TResponse>)requestHandler.GetType().GetMethod("HandleAsync").Invoke(requestHandler, new[] {request});          
            
        }
    }
}