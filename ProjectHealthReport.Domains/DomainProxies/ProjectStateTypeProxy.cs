using System.Collections.Generic;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Domains.DomainProxies
{
    public class ProjectStateTypeProxy : IMapFrom<ProjectStateType>, IMapTo<ProjectStateType>
    {
        public int Id { get; set; }
        public string State { get; set; }
        public ICollection<ProjectProxy> Projects { get; set; }
    }
}