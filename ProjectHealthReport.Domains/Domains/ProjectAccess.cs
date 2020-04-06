using System.Collections.Generic;
using System.Linq;
using ProjectHealthReport.Domains.Exceptions;
using ProjectHealthReport.Domains.Helpers;

namespace ProjectHealthReport.Domains.Domains
{
    public class ProjectAccess
    {
        public ProjectAccess()
        {
            
        }
        public ProjectAccess(int id, int projectId, string email, string role, List<(string, string)> userRoleList, Project project)
        {
            Id = id;
            ProjectId = projectId;
            Email = email;
            Role = role;
            Project = project;
            
            Validate(userRoleList);
        }

        public int Id { get; private set; }
        public int ProjectId { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }
        public Project Project { get; private set; }

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