using System;
using System.Threading.Tasks;

namespace ResponsibilityChain
{
    public interface IHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        Task HandleAsync(TRequest request);
    }

    public class Handler<TRequest, TResponse> : IHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public virtual async Task HandleAsync(TRequest request)
        {
            if (request == null)
            {
                throw new NullReferenceException("Cannot handle null request");
            }
            
            if (Next == null)
            {
                throw new NullHandlerException($"The next handler of {this.GetType()} is null");
            }

            // Console.WriteLine($"Type of Next is {Next.GetType()}");
            await Next.HandleAsync(request);
        }

        public async Task HandleBranchAsync(TRequest request)
        {
            if (request == null)
            {
                throw new NullReferenceException("Cannot handle null request");
            }
            
            if (Branch != null)
            {
                await Branch.HandleAsync(request);
            }
        }

        public void Add(Handler<TRequest, TResponse> handler)
        {
            Next = AddHandler(Next, handler);
        }

        public void AddBranch(Handler<TRequest, TResponse> handler)
        {
            Branch = AddHandler(Branch, handler);
        }

        Handler<TRequest, TResponse> AddHandler(Handler<TRequest, TResponse> handler, Handler<TRequest, TResponse> handlerToAdd)
        {
            if (handlerToAdd == null)
            {
                throw new NullHandlerException("Cannot add null handler.");
            }
            
            if (handler == null)
            {
                handler = handlerToAdd;
            }
            else
            {
                var temp = handler;
                while (temp.Next != null)
                {
                    temp = temp.Next;
                }

                temp.Next = handlerToAdd;
            }

            return handler;
        }

        public Handler<TRequest, TResponse> Branch { get; set; }

        public Handler<TRequest, TResponse> Next { get; set; }
    }
}