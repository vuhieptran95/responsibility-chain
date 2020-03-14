using System.Collections.Generic;

namespace ResponsibilityChain.Business.RequestContexts
{
    public class RequestContext
    {
        public string UserId { get; set; } = "unidentified";
        public string UserName { get; set; } = "unidentified";
        public string UserRole { get; set; } = "unidentified";
        public string UserEmail { get; set; } = "unidentified";
        public object Request { get; set; }
        public object Response { get; set; }
        public Dictionary<object, object> Infos { get; set; }
        public List<string> AccessRights { get; set; } = new List<string>();
    }
}