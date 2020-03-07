using System.Threading.Tasks;

namespace ResponsibilityChain
{
    public class BranchHandler<TRequest, TResponse> : Handler<TRequest, TResponse>
    {
        public override async Task<TResponse> HandleAsync(TRequest request)
        {
            if (Branch != null)
            {
                await Branch.HandleAsync(request);
            }

            return await base.HandleAsync(request);
        }

        public void AddBranchHandler(Handler<TRequest, TResponse> handler)
        {
            var defaultBranchHandler = new DefaultBranchHandler<TRequest, TResponse>();
            if (Branch == null)
            {
                Branch = handler;
                Branch.Next = defaultBranchHandler;
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