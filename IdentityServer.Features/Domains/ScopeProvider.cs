using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Features.Domains
{
    public class ScopeProvider
    {
        public ScopeProvider()
        {
            Scopes = new HashSet<Scope>();
        }
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Scope> Scopes { get; set; }
    }
}