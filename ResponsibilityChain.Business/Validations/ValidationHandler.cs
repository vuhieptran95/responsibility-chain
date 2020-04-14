namespace ResponsibilityChain.Business.Validations
{
    public class ValidationHandler<TRequest, TResponse> : Handler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
    }
}