namespace IdentityServer.Features.Domains
{
    public class PolicyScope
    {
        public PolicyScope(string policyId, string scopeId)
        {
            PolicyId = policyId;
            ScopeId = scopeId;
        }

        public string PolicyId { get; set; }
        public string ScopeId { get; set; }
        public Policy Policy { get; set; }
        public Scope Scope { get; set; }
    }
}