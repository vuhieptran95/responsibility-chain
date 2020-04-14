#nullable enable
using System;

namespace ResponsibilityChain.Business.AuthorizationConfigs
{
    public class AuthorizationConfigException : Exception
    {
        public AuthorizationConfigException(string? message) : base(message)
        {
            
        }
    }
}