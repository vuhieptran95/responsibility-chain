namespace ResponsibilityChain.Test
{
    public class Request : IRequest<Response>
    {
        public Response Response { get; set; }
    }
}