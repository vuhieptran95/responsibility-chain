using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Features.Business.Clients;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using ResponsibilityChain.Business;

namespace IdentityServer.Configs
{
    public class ClientStore : IClientStore
    {
        private readonly IMediator _mediator;

        public ClientStore(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var clients = await _mediator.SendAsync(new GetClients());

            return clients.Clients.FirstOrDefault(c => c.ClientId == clientId);
        }
    }
}