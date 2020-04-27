using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Features.Domains
{
    public class Policy
    {
        public Policy()
        {
            PolicyScopes = new HashSet<PolicyScope>();
            UserPolicies = new HashSet<UserPolicy>();
        }

        [Key]
        public string Id { get; set; }

        public string Name { get; set; }
        public ICollection<PolicyScope> PolicyScopes { get; set; }
        public ICollection<UserPolicy> UserPolicies { get; set; }
    }
}