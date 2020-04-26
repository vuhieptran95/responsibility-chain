using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Features;
using IdentityServer.Features.Business.ScopeProviders;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using ResponsibilityChain.Business;

namespace IdentityServer.Configs
{
    public class ResourceStore : IResourceStore
    {
        private readonly IMediator _mediator;

        public ResourceStore(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var identityResources = Config.Ids;
            var id = identityResources.Where(i => scopeNames.Contains(i.Name));

            return Task.FromResult(id);
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var dto = await _mediator.SendAsync(new GetScopes());
            var api = from a in dto.ApiResources
                let scopes = (from s in a.Scopes where scopeNames.Contains(s.Name) select s)
                where scopes.Any()
                select a;

            return api;
        }

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            var dto = await _mediator.SendAsync(new GetScopes());
            return dto.ApiResources.FirstOrDefault(r => r.Name == name);
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            var dto = await _mediator.SendAsync(new GetScopes());
            var identityResources = Config.Ids;

            return new Resources(identityResources, dto.ApiResources);
        }
    }
}