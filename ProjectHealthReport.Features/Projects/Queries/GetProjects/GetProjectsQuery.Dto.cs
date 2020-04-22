using System;
using System.Collections.Generic;
using AutoMapper;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjects
{
    public partial class GetProjectsQuery
    {
        public class Dto
        {
            public Dto(IEnumerable<ProjectDto> projects)
            {
                Projects = projects;
            }

            public IEnumerable<ProjectDto> Projects { get; set; }

            public class ProjectDto : IMapFrom<Project>
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Code { get; set; }
                public string Division { get; set; }
                public string KeyAccountManager { get; set; }
                public string DeliveryResponsibleName { get; set; }
                public bool PhrRequired { get; set; }
                public bool DodRequired { get; set; }
                public bool DmrRequired { get; set; }
                public DateTime? DmrRequiredFrom { get; set; }
                public DateTime? DmrRequiredTo { get; set; }
                public string ProjectStateType { get; set; }
                public int ProjectStateTypeId { get; set; }

                public void MappingFrom(Profile profile)
                {
                    profile.CreateMap<Domains.Domains.Project, ProjectDto>()
                        .ForMember(des =>
                            des.ProjectStateType, opt =>
                            opt.MapFrom(src => src.ProjectStateType.State));
                }
            }
        }
    }
}