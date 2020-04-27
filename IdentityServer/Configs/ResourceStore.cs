using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Features;
using IdentityServer.Features.Business.ScopeProviders;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using ResponsibilityChain.Business;

namespace IdentityServer.Configs
{
    public class ProfileService: IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            throw new System.NotImplementedException();
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ResourceStore : IResourceStore
    {
        private readonly IMediator _mediator;
        // private IEnumerable<IdentityResource> _identityResources;
        // private IEnumerable<ApiResource> _apiResources;

        public ResourceStore(IMediator mediator)
        {
            _mediator = mediator;
            // _apiResources = (_mediator.SendAsync(new GetScopes())).GetAwaiter().GetResult().ApiResources;
            // _identityResources = Config.Ids;
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var identity = from i in Config.Ids
                where scopeNames.Contains(i.Name)
                select i;

            return Task.FromResult(identity);

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