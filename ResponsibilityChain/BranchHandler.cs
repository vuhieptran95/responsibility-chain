using System.Threading.Tasks;

namespace ResponsibilityChain
{
    public class BranchHandler<TRequest, TResponse> : Handler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        public override async Task HandleAsync(TRequest request)
        {
            if (Branch != null)
            {
                await Branch.HandleAsync(request);
            }

            await base.HandleAsync(request);
        }

        public void AddBranchHandler(Handler<TRequest, TResponse> handler)
        {
            var defaultBranchHandler = new DefaultBranchHandler<TRequest, TResponse>();
            if (Branch == null)
            {
                Branch = handler;

                if (!(handler is DefaultBranchHandler<TRequest, TResponse>))
                {
                    Branch.Next = defaultBranchHandler;
                }
            }
            else
            {
                var temp = Branch;
                while (temp.Next != null)
                {
                    if (temp.Next is DefaultBranchHandler<TRequest, TResponse>)
                    {
                        temp.Next = null;
                        break;
                    }
                    temp = temp.Next;
                }

                temp.Next = handler;
                temp.Next.Next = defaultBranchHandler;
            }
        }

        public Handler<TRequest, TResponse> Branch { get; set; }
    }
}