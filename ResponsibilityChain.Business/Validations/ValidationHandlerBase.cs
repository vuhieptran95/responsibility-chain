using System.Linq;
using System.Threading.Tasks;

namespace ResponsibilityChain.Business.Validations
{
    public class ValidationHandlerBase<TRequest, TResponse> : Handler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        private readonly IPreValidation<TRequest, TResponse>[] _preValidations;
        private readonly IPostValidation<TRequest, TResponse>[] _postValidations;

        public ValidationHandlerBase(IPreValidation<TRequest, TResponse>[] preValidations, IPostValidation<TRequest, TResponse>[] postValidations)
        {
            _preValidations = preValidations;
            _postValidations = postValidations;
        }

        public override async Task HandleAsync(TRequest request)
        {
            _preValidations.ToList().ForEach(async v => { await v.HandleAsync(request); });
            
            await base.HandleAsync(request);
            
            _postValidations.ToList().ForEach(async v => { await v.HandleAsync(request);});
        }
    }

    public interface IPreValidation<TRequest, TResponse> : IHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
    }
    
    public interface IPostValidation<TRequest, TResponse> : IHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
    }
}