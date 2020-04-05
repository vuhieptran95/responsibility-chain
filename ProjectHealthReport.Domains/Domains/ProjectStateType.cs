using System.Collections.Generic;

namespace ProjectHealthReport.Domains.Domains
{
    public class ProjectStateType
    {
        public ProjectStateType()
        {
            Projects = new HashSet<Project>();
        }
        public int Id { get; set; }
        public string State { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}