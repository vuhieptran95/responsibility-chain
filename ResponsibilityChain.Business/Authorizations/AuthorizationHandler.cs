namespace ResponsibilityChain.Business.Authorizations
{
    public class AuthorizationHandler<TRequest, TResponse> : BranchHandler<TRequest, TResponse> where TRequest: IRequest<TResponse> 
    {
    }
}