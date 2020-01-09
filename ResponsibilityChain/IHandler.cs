using System.Threading.Tasks;

namespace ResponsibilityChain
{
    public interface IHandler<TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request);
        IHandler<TRequest, TResponse> Next { get; set; }
        IHandler<TRequest, TResponse> AddHandler(IHandler<TRequest, TResponse> handler);
    }
}