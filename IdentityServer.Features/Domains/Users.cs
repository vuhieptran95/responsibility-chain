using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Features.Domains
{
    public class Users
    {
        public Users()
        {
            UserScopes = new HashSet<UserScope>();
        }

        [Key]
        public string Username { get; set; }
        public string Role { get; set; }
        public ICollection<UserScope> UserScopes { get; set; }

        public void ValidateRole()
        {
            if (!Roles.AssignableRoles.Contains(Role))
            {
                throw new ValidationException("Invalid role");
            }
        }
    }
}