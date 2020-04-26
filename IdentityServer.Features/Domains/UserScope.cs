namespace IdentityServer.Features.Domains
{
    public class UserScope
    {
        public string Username { get; set; }
        public string ScopeId { get; set; }

        public Scope Scope { get; set; }
        public Users User { get; set; }
    }
}