using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Features.Domains;
using Microsoft.EntityFrameworkCore;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace IdentityServer.Features.Business.Scopes
{
    public class GetScopesByPolicyId : Request<GetScopesByPolicyId.Dto>
    {
        public string PolicyId { get; set; }

        public class Dto
        {
            public IEnumerable<string> Scopes { get; set; }
        }

        public class Handler : IExecution<GetScopesByPolicyId, Dto>
        {
            private readonly IdPDbContext _dbContext;

            public Handler(IdPDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task HandleAsync(GetScopesByPolicyId request)
            {
                var scopes = await _dbContext.Policies.Where(p => p.Id == request.PolicyId)
                    .SelectMany(p => p.PolicyScopes).Select(ps => ps.ScopeId).Distinct().OrderBy(s => s).ToListAsync();

                request.Response = new Dto() {Scopes = scopes};
            }
        }
    }
}