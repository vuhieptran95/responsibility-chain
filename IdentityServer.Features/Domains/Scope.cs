using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Features.Domains
{
    public class Scope
    {
        public Scope()
        {
            UserScopes = new HashSet<UserScope>();
            ClientScopes = new HashSet<ClientScope>();
        }

        [Key] public string Id { get; private set; }

        public string ScopeProviderId { get; set; }
        public string Resource { get; set; }
        public string Action { get; set; }
        public ScopeProvider ScopeProvider { get; set; }
        public ICollection<UserScope> UserScopes { get; set; }
        public ICollection<ClientScope> ClientScopes { get; set; }

        public void CreateScopeId(bool isIdentity = false)
        {
            if (isIdentity)
            {
                Id = Resource;
            }
            else
            {
                Id = $"{ScopeProviderId}.{Resource}:{Action}";
            }
        }

        public void ValidateAction()
        {
            if (Action == Actions.Read || Action == Actions.Create || Action == Actions.Update ||
                Action == Actions.Delete || Action == Actions.All)
            {
                return;
            }

            throw new ValidationException("Invalid Action");
        }
    }
}