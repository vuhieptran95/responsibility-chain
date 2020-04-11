using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.DomainProxies;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Mappings;
using ProjectHealthReport.Features.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Projects.Commands
{
    public class EditProjectMasterDataCommand : IRequest<int>, IMapTo<ProjectProxy>
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

                if (request.PhrRequired && !projectProxy.PhrRequired)
                {
                    projectProxy.PhrRequiredFrom = DateTime.Now;
                }

                _mapper.Map(request, projectProxy);

                var userRoleList = await _mediator.SendAsync(new GetListUserRoleQuery());
                var project = _mapper.Map<Project>(projectProxy, opt =>
                    opt.Items[MiscHelper.UserRoleListCtor] = userRoleList);

                _dbContext.Projects.Update(project);
                await _dbContext.SaveChangesAsync();

                return request.Id;
            }
        }
    }
}