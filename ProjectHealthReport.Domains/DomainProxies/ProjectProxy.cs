using System;
using System.Collections.Generic;
using AutoMapper;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Domains.DomainProxies
{
    public class ProjectProxy : IMapFrom<Project>, IMapTo<Project>
    {
        public ProjectProxy()
        {
            
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

        public string PlatformVersion { get; set; }

        public string JIRALink { get; set; }

        public string SourceCodeLink { get; set; }

        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public DateTime? PhrRequiredFrom { get; set; }
        public DateTime? DmrRequiredFrom { get; set; }
        public DateTime? DmrRequiredTo { get; set; }
        public ProjectStateTypeProxy ProjectStateType { get; set; }
        public ICollection<StatusProxy> Statuses { get; set; }
        public ICollection<BacklogItemProxy> BacklogItems { get; set; }
        public ICollection<QualityReportProxy> QualityReports { get; set; }
        public ICollection<DivisionProjectStatusProxy> DivisionProjectStatuses { get; set; }
        public ICollection<WeeklyReportStatusProxy> WeeklyReportStatuses { get; set; }
        public ICollection<ProjectAccessProxy> ProjectAccesses { get; set; }
        public ICollection<DoDReportProxy> DoDReports { get; set; }

        

        public void MappingTo(Profile profile)
        {
            profile.CreateMap<ProjectProxy, Project>()
                .ForCtorParam("userRoleList", opt =>
                    opt.MapFrom((p, context) => context.Items[MiscHelper.UserRoleListCtor]));
        }
    }
}