using System.Collections.Generic;
using System.Linq;
using ProjectHealthReport.Domains.Exceptions;
using ProjectHealthReport.Domains.Helpers;

namespace ProjectHealthReport.Domains.Domains
{
    public class ProjectAccess
    {
        private int _id;
        private int _projectId;
        private string _email;
        private string _role;
        private Project _project;

        public ProjectAccess()
        {
            
        }
        public ProjectAccess(int id, int projectId, string email, string role, List<(string, string)> userRoleList, Project project)
        {
            _id = id;
            _projectId = projectId;
            _email = email;
            _role = role;
            _project = project;
            
            Validate(userRoleList);
        }

        public int Id => _id;

        public int ProjectId => _projectId;

        public string Email => _email;

        public string Role => _role;

        public Project Project => _project;

        public void Validate(List<(string, string)> userRoleList)
        {
            if (!userRoleList.Select(i => i.Item1).Contains(Email))
            {
                DomainExceptionCode.Throw(DomainError.D014, this);
            }

            if (Role != AuthorizationHelper.RolePic)
            {
                DomainExceptionCode.Throw(DomainError.D015, this);
            }
        }
    }
}