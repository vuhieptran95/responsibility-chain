using System.Linq;
using System.Threading.Tasks;

namespace ResponsibilityChain.Business.Validations
{
    public class ValidationHandlerBase<TRequest, TResponse> : Handler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        private readonly PreValidation<TRequest, TResponse>[] _preValidations;
        private readonly PostValidation<TRequest, TResponse>[] _postValidations;

        public ValidationHandlerBase(PreValidation<TRequest, TResponse>[] preValidations, PostValidation<TRequest, TResponse>[] postValidations)
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

    public class PreValidation<TRequest, TResponse> : Handler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public override Task HandleAsync(TRequest request)
        {
            return Task.CompletedTask;
        }
    }
    
    public class PostValidation<TRequest, TResponse> : Handler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public override Task HandleAsync(TRequest request)
        {
            return Task.CompletedTask;
        }
    }
}