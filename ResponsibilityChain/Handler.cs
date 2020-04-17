using System;
using System.Threading.Tasks;

namespace ResponsibilityChain
{
    public interface IHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        Task HandleAsync(TRequest request);
    }

    public class Handler<TRequest, TResponse> : IHandler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        public virtual Task HandleAsync(TRequest request)
        {
            if (Next == null)
            {
                throw new NullHandlerException($"The next handler of {this.GetType()} is null");
            }

            Console.WriteLine($"Type of Next is {Next.GetType()}");
            return Next.HandleAsync(request);
        }

        public void AddHandler(Handler<TRequest, TResponse> handler)
        {
            if (Next == null)
            {
                Next = handler;
            }
            else
            {
                var temp = Next;
                while (temp.Next != null)
                {
                    temp = temp.Next;
                }

                temp.Next = handler;
            }
        }

        public Handler<TRequest, TResponse> Next { get; set; }
    }
}