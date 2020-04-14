using System.Collections.Generic;

namespace ProjectHealthReport.Domains.Domains
{
    public class ProjectStateType
    {
        private ICollection<Project> _projects;
        private string _state;
        private int _id;

        public ProjectStateType()
        {
            _projects = new HashSet<Project>();
        }

        public ProjectStateType(string state, int id)
        {
            _state = state;
            _id = id;
        }

        public ProjectStateType(ICollection<Project> projects, string state, int id) : this()
        {
            _projects = projects;
            _state = state;
            _id = id;
        }

        public int Id => _id;

        public string State => _state;

        public IEnumerable<Project> Projects => _projects;
    }
}