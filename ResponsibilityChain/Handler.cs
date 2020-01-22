using System.Threading.Tasks;

namespace ResponsibilityChain
{
    public class Handler<TRequest, TResponse> : IHandler<TRequest, TResponse>
    {
        public virtual Task<TResponse> HandleAsync(TRequest request)
        {
            if (Next == null)
            {
                throw new NullHandlerException($"The next handler of {nameof(Next)} is null");
            }
            return Next.HandleAsync(request);
        }

        public IHandler<TRequest, TResponse> AddHandler(IHandler<TRequest, TResponse> handler)
        {
            if (Next == null)
            {
                Next = handler;
            }
            else
            {
                Next.Next = Next.AddHandler(handler);
            }

            return Next;
        }

        public IHandler<TRequest, TResponse> Next { get; set; }
    }
}