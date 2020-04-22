using System.Linq;
using System.Threading.Tasks;

namespace ResponsibilityChain.Business.Authorizations
{
    public class AuthorizationHandler<TRequest, TResponse> : Handler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
    }

    public interface IPreAuthorization<TRequest, TResponse> : IPreHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
    }

    public interface IPostAuthorization<TRequest, TResponse> : IPreHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
    }
}