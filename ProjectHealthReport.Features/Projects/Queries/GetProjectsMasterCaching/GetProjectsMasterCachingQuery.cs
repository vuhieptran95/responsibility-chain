using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagePack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Features.Projects.Commands;
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.Events;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjectsMasterCaching
{
    public partial class GetProjectsMasterCachingQuery : Request<CacheResponse<GetProjectsMasterCachingQuery.Dto>>
    {
        public class Dto
        {
            public Dto()
            {
                Projects = new List<Project>();
            }

            public IEnumerable<Project> Projects { get; set; }
        }

        [MessagePackObject]
        public class Project
        {
            public Project()
            {
                UsersAllowed = new List<string>();
                Accesses = new List<string>();
            }

            [Key(0)] public int Id { get; set; }
            [Key(1)] public string Code { get; set; }
            [Key(2)] public string Name { get; set; }
            [Key(3)] public bool DmrRequired { get; set; }
            [Key(4)] public bool PhrRequired { get; set; }
            [Key(5)] public bool DoDRequired { get; set; }
            [Key(6)] public string State { get; set; }
            [Key(7)] public string Division { get; set; }
            [Key(8)] public string Pm { get; set; }
            [Key(9)] public IEnumerable<string> Accesses { get; set; }
            [Key(10)] public IEnumerable<string> UsersAllowed { get; set; }
        }

        public class Handler : IExecution<GetProjectsMasterCachingQuery, CacheResponse<Dto>>
        {
            private readonly ReportDbContext _dbContext;

            public Handler(ReportDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task HandleAsync(GetProjectsMasterCachingQuery request)
            {
                var projects = await _dbContext.Projects.Select(p => new Project()
                {
                    Id = p.Id, Code = p.Code, Name = p.Name, DmrRequired = p.DmrRequired, PhrRequired = p.PhrRequired,
                    DoDRequired = p.DodRequired, State = p.ProjectStateType.State, Division = p.Division,
                    Pm = p.DeliveryResponsibleName,
                    Accesses = p.ProjectAccesses.Where(pa => pa.Role == AuthorizationHelper.RolePic)
                        .Select(pa => pa.Email)
                }).ToListAsync();

                projects.ForEach(d => d.UsersAllowed = d.Accesses.Union(new List<string>() {d.Pm}));

                request.Response = new CacheResponse<Dto>();
                request.Response.SetResponse(new Dto() {Projects = projects});
            }
        }

        public class CacheConfig : ICacheConfig<GetProjectsMasterCachingQuery>
        {
            public bool IsCacheEnabled { get; } = true;
            public DateTimeOffset CacheDateTimeOffset { get; } = DateTimeOffset.Now.AddDays(1);

            public string GetCacheKey(GetProjectsMasterCachingQuery request)
            {
                return typeof(GetProjectsMasterCachingQuery).FullName;
            }
        }
    }
}