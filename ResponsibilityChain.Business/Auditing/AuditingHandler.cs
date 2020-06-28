namespace ResponsibilityChain.Business.Auditing
{
    public class AuditingHandler<TRequest, TResponse> : Handler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        public AuditingHandler(IPreAudit<TRequest, TResponse>[] preAudits, IPostAudit<TRequest, TResponse>[] postAudits)
        {
            foreach (var preAudit in preAudits)
            {
                AddHandler(preAudit);
            }

            foreach (var postAudit in postAudits)
            {
                AddHandler(postAudit);
            }
        }
    }
    
    public interface IPreAudit<TRequest, TResponse> : IPreHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
    }
    
    public interface IPostAudit<TRequest, TResponse> : IPostHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
    }
}