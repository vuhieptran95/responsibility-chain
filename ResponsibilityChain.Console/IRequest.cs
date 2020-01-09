namespace ResponsibilityChain.Console
{
    public interface IRequest
    {
        
    }

    public class Request : IRequest
    {
        public string Role { get; set; }
    }

    public class RequestChild : Request
    {
        
    }
}