using AutoMapper;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Domains.DomainProxies
{
    public class ProjectAccessProxy : IMapFrom<ProjectAccess>, IMapTo<ProjectAccess>
    {
        public ProjectAccessProxy()
        {
            
        }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public ProjectProxy Project { get; set; }
        
        public void MappingTo(Profile profile)
        {
            profile.CreateMap<ProjectAccessProxy, ProjectAccess>()
                .ForCtorParam("userRoleList", opt =>
                    opt.MapFrom((p, context) => context.Items[MiscHelper.UserRoleListCtor]));
        }
    }
}