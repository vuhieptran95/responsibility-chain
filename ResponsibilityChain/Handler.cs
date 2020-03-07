using System;
using System.Threading.Tasks;

namespace ResponsibilityChain
{
    public class Handler<TRequest, TResponse>
    {
        public virtual Task<TResponse> HandleAsync(TRequest request)
        {
            if (Next == null)
            {
                throw new NullHandlerException($"The next handler of {nameof(Next)} is null");
            }

            Console.WriteLine($"Type of Next is {Next.GetType()}");
            return Next.HandleAsync(request);
        }

        public Handler<TRequest, TResponse> AddHandler(Handler<TRequest, TResponse> handler)
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

        public Handler<TRequest, TResponse> Next { get; set; }
    }
}