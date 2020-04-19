using System.Threading.Tasks;

namespace ResponsibilityChain.Business.Authorizations
{
    public class AuthorizationHandler<TRequest, TResponse> : Handler<TRequest, TResponse> where TRequest: IRequest<TResponse> 
    {
    }
}