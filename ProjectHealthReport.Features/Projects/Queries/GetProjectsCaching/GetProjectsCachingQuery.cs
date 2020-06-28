using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Features.Projects.Commands;
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.Events;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjectsCaching
{
    public partial class GetProjectsCachingQuery : Request<GetProjectsCachingQuery.Dto>
    {
        public class Dto
        {
            public List<Project> Projects { get; set; }
        }

        public class Project
        {
            public Project()
            {
                Pics = new List<string>();
            }
            public int Id { get; set; }

            public string Name { get; set; }

            public string Code { get; set; }

            public string Division { get; set; }

            public string KeyAccountManager { get; set; }

            public bool PhrRequired { get; set; }

            public bool DmrRequired { get; set; }

            public bool DodRequired { get; set; }

            public int ProjectStateTypeId { get; set; }

            public string DeliveryResponsibleName { get; set; }
            public List<string> Pics { get; set; }
            public DateTime ProjectStartDate { get; set; }
        }
        
        public class CacheConfig : ICacheConfig<GetProjectsCachingQuery>
        {
            public bool IsCacheEnabled { get; } = true;
            public DateTimeOffset CacheDateTimeOffset { get; } = DateTimeOffset.Now.AddDays(1);
            public string GetCacheKey(GetProjectsCachingQuery request)
            {
                return typeof(GetProjectsCachingQuery).FullName;
            }
        }
        
        public class Handler : IExecution<GetProjectsCachingQuery, Dto>
        {
            private readonly ReportDbContext _dbContext;

            public Handler(ReportDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task HandleAsync(GetProjectsCachingQuery request)
            {
                var projects = await _dbContext.Projects.Select(p => new Project()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    Division = p.Division,
                    DmrRequired = p.DmrRequired,
                    DodRequired = p.DodRequired,
                    PhrRequired = p.PhrRequired,
                    DeliveryResponsibleName = p.DeliveryResponsibleName,
                    KeyAccountManager = p.KeyAccountManager,
                    ProjectStartDate = p.ProjectStartDate,
                    ProjectStateTypeId = p.ProjectStateTypeId,
                    Pics = p.ProjectAccesses.Select(a => a.Email).ToList()
                }).ToListAsync();
                
                projects.ForEach(p => p.Pics.Add(p.DeliveryResponsibleName));
                
                request.Response = new Dto(){Projects = projects};
            }
        }
    }
}