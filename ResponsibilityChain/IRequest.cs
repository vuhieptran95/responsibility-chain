using System.Threading.Tasks;

namespace ResponsibilityChain
{
    public interface IRequest<TResponse>
    {
        TResponse Response { get; set; }
    }
    
    
}