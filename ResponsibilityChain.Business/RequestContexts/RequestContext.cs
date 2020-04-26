using System.Collections.Generic;

namespace ResponsibilityChain.Business.RequestContexts
{
    public interface IRequestContext
    {
        string UserId { get; set; }
        string Username { get; set; }
        string UserRole { get; set; }
        string UserEmail { get; set; }
        Dictionary<string, object> TempData { get; }
        List<string> AccessRights { get; set; }
    }

    public class RequestContext : IRequestContext
    {
        public RequestContext()
        {
            TempData = new Dictionary<string, object>();
        }
        public string UserId { get; set; } = "unidentified";
        public string Username { get; set; } = "PM2";
        public string UserRole { get; set; } = "";
        public string UserEmail { get; set; } = "unidentified";
        public Dictionary<string, object> TempData { get; }
        public List<string> AccessRights { get; set; } = new List<string>();
    }
}