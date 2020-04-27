namespace IdentityServer.Features.Domains
{
    public class UserPolicy
    {
        public string Username { get; set; }
        public string PolicyId { get; set; }
        public Users User { get; set; }
        public Policy Policy { get; set; }
    }
}