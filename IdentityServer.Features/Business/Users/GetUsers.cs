using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer.Features.Domains;
using IdentityServer4.Test;
using Microsoft.EntityFrameworkCore;
using ResponsibilityChain;
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace IdentityServer.Features.Business.Users
{
    public class GetUsers : Request<GetUsers.Dto>
    {
        public class Dto
        {
            public IEnumerable<TestUser> Users { get; set; }
        }
        
        public class CacheConfig: ICacheConfig<GetUsers>
        {
            public bool IsCacheEnabled { get; } = true;
            public DateTimeOffset CacheDateTimeOffset { get; } = DateTimeOffset.Now.AddDays(1);
            public string GetCacheKey(GetUsers request)
            {
                return request.GetType().FullName;
            }
        }

        public class Handler: IExecution<GetUsers, Dto>
        {
            private readonly IdPDbContext _dbContext;

            public Handler(IdPDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task HandleAsync(GetUsers request)
            {
                var testUsers = (await _dbContext.Users
                    .Include(u => u.UserPolicies)
                    .ThenInclude(up => up.Policy)
                    .ThenInclude(p => p.PolicyScopes)
                    .ToListAsync()).Select(u => new TestUser()
                {
                    Username = u.Username,
                    Password = u.Username,
                    SubjectId = u.Username,
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, u.Username),
                        new Claim(JwtClaimTypes.Role, u.Role),
                        new Claim("rights", string.Join(' ', u.UserPolicies
                            .Select(up => up.Policy.PolicyScopes)
                            .SelectMany(scopes => scopes)
                            .Select(ps => ps.ScopeId)
                            .Distinct()
                        ))
                    }
                });
                
                request.Response = new Dto(){Users = testUsers};
            }
        }
    }
}