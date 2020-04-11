using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Divisions.Queries
{
    public class GetDivisionIndexQuery : IRequest<GetDivisionIndexQuery.Dto>
    {
        public class Handler : ExecutionHandlerBase<GetDivisionIndexQuery, Dto>
        {
            public override Task<Dto> HandleAsync(GetDivisionIndexQuery request)
            {
                var listDivisions = new List<Dto.Division>
                {
                    new Dto.Division {Name = "Tyr", ManagerName = "DM8"},
                    new Dto.Division {Name = "Frey", ManagerName = "DM3"},
                    new Dto.Division {Name = "Odin", ManagerName = "DM7"},
                    new Dto.Division {Name = "Thor", ManagerName = "DM6"},
                    new Dto.Division {Name = "Baldur", ManagerName = "DM4"},
                    new Dto.Division {Name = "HCMC", ManagerName = "DM5"},
                    new Dto.Division {Name = "AMS 24/7", ManagerName = "DM1"},
                    new Dto.Division {Name = "Marketing", ManagerName = "DM2"},
                };

                var dto = new Dto
                {
                    Divisions = listDivisions
                };

                return Task.FromResult(dto);
            }
        }

        public class Dto
        {
            public IEnumerable<Division> Divisions { get; set; }

            public class Division
            {
                public string Name { get; set; }
                public string ManagerName { get; set; }
            }
        }
    }
}