using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Features.Domains;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using ResponsibilityChain;
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;
using Scope = IdentityServer4.Models.Scope;

namespace IdentityServer.Features.Business.ScopeProviders
{
    public class GetScopes : Request<GetScopes.Dto>
    {
        public class Dto
        {
            public IEnumerable<ApiResource> ApiResources { get; set; }
        }
        
        public class CacheConfig: ICacheConfig<GetScopes>
        {
            public bool IsCacheEnabled { get; } = true;
            public DateTimeOffset CacheDateTimeOffset { get; } = DateTimeOffset.Now.AddDays(1);
            public string GetCacheKey(GetScopes request)
            {
                return request.GetType().FullName;
            }
        }

        public class Handler : IExecution<GetScopes, Dto>
        {
            private readonly IdPDbContext _dbContext;

            public Handler(IdPDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task HandleAsync(GetScopes request)
            {
                var providers = await _dbContext.ScopeProviders.
                    Include(s => s.Scopes).ToListAsync();

                var apiResources = providers
                    .Select(p => new ApiResource(p.Id, p.Name)
                {
                    Scopes = p.Scopes.Where(s => s.ScopeProviderId != "idp").Select(s => new Scope()
                    {
                        Name = s.Id
                    }).ToList()
                });
                
                request.Response = new Dto(){ApiResources = apiResources};
            }
        }
    }
}