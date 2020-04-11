using System.Collections.Generic;

namespace ResponsibilityChain.Business.RequestContexts
{
    public class RequestContext
    {
        public RequestContext()
        {
            TempData = new Dictionary<string, object>();
        }
        public string UserId { get; set; } = "unidentified";
        public string UserName { get; set; } = "unidentified";
        public string UserRole { get; set; } = "unidentified";
        public string UserEmail { get; set; } = "unidentified";
        public Dictionary<string, object> TempData { get; }
        public List<string> AccessRights { get; set; } = new List<string>();
    }
}