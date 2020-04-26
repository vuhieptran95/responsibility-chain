    using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Features.Domains
{
    public class Client
    {
        public Client()
        {
            ClientScopes = new HashSet<ClientScope>();
        }
        [Key]
        public string Id { get; set; }
        public string Secret { get; set; }
        public string RedirectedUri { get; set; }
        public bool UserInteractionRequired { get; set; }

        public ICollection<ClientScope> ClientScopes { get; set; }
        
        public void ValidateClientType()
        {
            if (UserInteractionRequired && string.IsNullOrEmpty(RedirectedUri))
            {
                throw new ValidationException("If UserInteraction is required, RedirectedUri must have value");
            }
        }
    }
}