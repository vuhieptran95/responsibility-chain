using System.Collections.Generic;
using System.Linq;
using ProjectHealthReport.Domains.Exceptions;
using ProjectHealthReport.Domains.Helpers;

namespace ProjectHealthReport.Domains.Domains
{
    public class ProjectAccess
    {
        protected int _id;
        protected int _projectId;
        protected string _email;
        protected string _role;
        protected Project _project;

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

        public ProjectAccess(int id, int projectId, string email, string role)
        {
            _id = id;
            _projectId = projectId;
            _email = email;
            _role = role;
        }

        public int Id => _id;

        public int ProjectId => _projectId;

        public string Email => _email;

        public string Role => _role;

        public Project Project => _project;

        public void Validate(List<(string Email, string Role)> userRoleList)
        {
            if (!userRoleList.Select(i => i.Email).Contains(Email))
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