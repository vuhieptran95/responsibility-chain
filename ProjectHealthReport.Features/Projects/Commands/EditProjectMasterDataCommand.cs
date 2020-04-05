using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Features.Common.Mappings;
using ProjectHealthReport.Features.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Projects.Commands
{
    public class EditProjectMasterDataCommand : IRequest<int>
    {
        public int Id { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string Code { get; set; }

        [Required] public string Division { get; set; }

        [Required] public string KeyAccountManager { get; set; }
        public bool PhrRequired { get; set; }
        public bool DmrRequired { get; set; }
        public bool DodRequired { get; set; }
        public int ProjectStateTypeId { get; set; }
        public string DeliveryResponsibleName { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public DateTime? DmrRequiredFrom { get; set; }
        public DateTime? DmrRequiredTo { get; set; }

        public List<(string, string)> UserRoleList { get; set; }

        
        public class ProjectProxy: IMapFrom<Project>
        {
            public int Id { get; set; }

            [Required] public string Name { get; set; }

            [Required] public string Code { get; set; }

            [Required] public string Division { get; set; }

            [Required] public string KeyAccountManager { get; set; }
            public bool PhrRequired { get; set; }
            public bool DmrRequired { get; set; }
            public bool DodRequired { get; set; }
            public int ProjectStateTypeId { get; set; }

            public string DeliveryResponsibleName { get; set; }

            public string PlatformVersion { get; set; }

            public string JIRALink { get; set; }

            public string SourceCodeLink { get; set; }

            public string Note { get; set; }
            public DateTime ProjectStartDate { get; set; }
            public DateTime? ProjectEndDate { get; set; }
            public DateTime? PhrRequiredFrom { get; set; }
            public DateTime? DmrRequiredFrom { get; set; }
            public DateTime? DmrRequiredTo { get; set; }
            public List<(string, string)> UserRoleList { get; set; }
            
            public void Mapping(Profile profile)
            {
                profile.CreateMap<EditProjectMasterDataCommand, ProjectProxy>();
                profile.CreateMap<Project, ProjectProxy>();
                profile.CreateMap<ProjectProxy, Project>()
                    .ConstructUsing(proxy => new Project(proxy.Id, proxy.Name, proxy.Code, proxy.Division, proxy.KeyAccountManager,
                        proxy.ProjectStartDate, proxy.PhrRequired, proxy.DmrRequired, proxy.DodRequired, proxy.ProjectStateTypeId,
                        proxy.UserRoleList, proxy.DeliveryResponsibleName, proxy.PlatformVersion, proxy.JIRALink,
                        proxy.SourceCodeLink,
                        proxy.Note, proxy.ProjectEndDate, proxy.PhrRequiredFrom, proxy.DmrRequiredFrom, proxy.DmrRequiredTo,
                        null, null, null, null, null,
                        null, null, null, null));
            }
        }

        public class Handler : ExecutionHandlerBase<EditProjectMasterDataCommand, int>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public Handler(ReportDbContext dbContext, IMapper mapper, IMediator mediator)
            {
                _dbContext = dbContext;
                _mapper = mapper;
                _mediator = mediator;
            }

            public override async Task<int> HandleAsync(EditProjectMasterDataCommand request)
            {
                var projectInDb = await _dbContext.Projects.AsNoTracking().SingleAsync(p => p.Id == request.Id);
                var projectProxy = _mapper.Map<ProjectProxy>(projectInDb);
                var phrRequiredProxy = projectProxy.PhrRequired;

                if (request.PhrRequired && !projectProxy.PhrRequired)
                {
                    projectProxy.PhrRequiredFrom = DateTime.Now;
                }
                
                _mapper.Map(request, projectProxy);

                projectProxy.UserRoleList = await _mediator.SendAsync(new GetListUserRoleQuery());
                var project = _mapper.Map<Project>(projectProxy);

                _dbContext.Projects.Update(project);
                await _dbContext.SaveChangesAsync();

                return request.Id;
            }
        }
    }
}