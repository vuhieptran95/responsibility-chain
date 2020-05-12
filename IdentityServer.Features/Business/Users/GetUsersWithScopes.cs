using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Features.Domains;
using Microsoft.EntityFrameworkCore;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace IdentityServer.Features.Business.Users
{
    public class GetUsersWithScopes : Request<GetUsersWithScopes.Dto>
    {
        public class Handler: IExecution<GetUsersWithScopes, Dto>
        {
            private readonly IdPDbContext _dbContext;

            public Handler(IdPDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task HandleAsync(GetUsersWithScopes request)
            {
                var users = await _dbContext.Users.Include(u => u.UserPolicies)
                    .ThenInclude(up => up.Policy)
                    .ThenInclude(p => p.PolicyScopes)
                    .ToListAsync();


                request.Response = new Dto(users.Select(u => new User()
                {
                    Username = u.Username,
                    Role = u.Role,
                    Policies = u.UserPolicies.Select(up => new Policy() {Id = up.Policy.Id, Name = up.Policy.Name}),
                    Scopes = u.UserPolicies.Select(up => up.Policy)
                        .SelectMany(p => p.PolicyScopes, (p, ps) => ps.ScopeId).Distinct().OrderBy(s => s)
                }).ToList());
            }
        }
        public class Dto
        {
            public Dto(List<User> users)
            {
                Users = users;
            }

            public List<User> Users { get; set; }
        }        
        
        public class User
        {
            public string Username { get; set; }
            public string Role { get; set; }
            public IEnumerable<Policy> Policies { get; set; }
            public IEnumerable<string> Scopes { get; set; }
        }
        
        public class Policy
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}