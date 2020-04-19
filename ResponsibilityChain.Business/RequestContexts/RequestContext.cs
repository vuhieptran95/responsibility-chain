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
        public string UserName { get; set; } = "PMO4";
        public string UserRole { get; set; } = "PMO Assistant";
        public string UserEmail { get; set; } = "unidentified";
        public Dictionary<string, object> TempData { get; }
        public List<string> AccessRights { get; set; } = new List<string>();
    }
}