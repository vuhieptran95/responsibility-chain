using System.Threading.Tasks;

namespace ResponsibilityChain
{
    public class BranchHandler<TRequest, TResponse> : Handler<TRequest, TResponse>
    {
        public override async Task<TResponse> HandleAsync(TRequest request)
        {
            if (NextBranch != null)
            {
                await NextBranch.HandleAsync(request);
            }

            return await base.HandleAsync(request);
        }

        public Handler<TRequest, TResponse> AddBranchHandler(BranchHandler<TRequest, TResponse> handler)
        {
            if (NextBranch == null)
            {
                NextBranch = handler;
            }
            else
            {
                NextBranch.Next = NextBranch.AddHandler(handler);
            }

            return Next;
        }

        public Handler<TRequest, TResponse> NextBranch { get; set; }
    }
}