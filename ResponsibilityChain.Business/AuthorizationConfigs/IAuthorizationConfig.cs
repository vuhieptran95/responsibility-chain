using System.Collections.Generic;
using System.Reflection;

namespace ResponsibilityChain.Business.AuthorizationConfigs
{
    public interface IAuthorizationConfig<TRequest>
    {
        List<(string[] Resources, string[] Actions)> GetAccessRights();
    }
    
    public class DefaultAuthorizationConfig<TRequest> : IAuthorizationConfig<TRequest>
    {
        public List<(string[] Resources, string[] Actions)> GetAccessRights()
        {
            return new List<(string[] Resources, string[] Actions)>();
        }
    }
}