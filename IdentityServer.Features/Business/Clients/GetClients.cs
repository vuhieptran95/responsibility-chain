using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Features.Business.ScopeProviders;
using IdentityServer.Features.Domains;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using ResponsibilityChain;
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;
using Client = IdentityServer4.Models.Client;

namespace IdentityServer.Features.Business.Clients
{
    public class GetClients : Request<GetClients.Dto>
    {
        public class Dto
        {
            public IEnumerable<Client> Clients { get; set; }
        }
        
        public class CacheConfig: ICacheConfig<GetClients>
        {
            public bool IsCacheEnabled { get; } = true;
            public DateTimeOffset CacheDateTimeOffset { get; } = DateTimeOffset.Now.AddDays(1);
            public string GetCacheKey(GetClients request)
            {
                return request.GetType().FullName;
            }
        }

        public class Handler : IExecution<GetClients, Dto>
        {
            private readonly IdPDbContext _dbContext;

            public Handler(IdPDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task HandleAsync(GetClients request)
            {
                var clients = (await _dbContext.Clients
                        .Include(c => c.ClientScopes)
                        .ToListAsync())
                    .Select(c =>
                    {
                        if (c.UserInteractionRequired)
                        {
                            return new Client()
                            {
                                ClientId = c.Id,
                                ClientSecrets = {new Secret(c.Secret.Sha256())},

                                AllowedGrantTypes = GrantTypes.Code,
                                RequireConsent = true,

                                RedirectUris = {c.RedirectedUri},

                                AlwaysIncludeUserClaimsInIdToken = true,

                                AllowOfflineAccess = true,

                                AllowedScopes = c.ClientScopes.Select(c => c.ScopeId).ToList()
                            };
                        }
                        else
                        {
                            return new Client()
                            {
                                ClientId = c.Id,
                                ClientSecrets = {new Secret(c.Secret.Sha256())},

                                AllowedGrantTypes = GrantTypes.ClientCredentials,
                    
                                AllowedScopes = c.ClientScopes.Select(c => c.ScopeId).ToList()
                            };
                        }
                    });
                
                request.Response = new Dto(){Clients = clients};
            }
        }
    }
}