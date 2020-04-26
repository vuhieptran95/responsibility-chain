namespace IdentityServer.Features.Domains
{
    public class ClientScope
    {
        public string ClientId { get; set; }
        public string ScopeId { get; set; }

        public Scope Scope { get; set; }
        public Client Client { get; set; }
    }
}