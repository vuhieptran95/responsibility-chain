using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Features.Common.Mappings;
using ProjectHealthReport.Features.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Projects.Commands
{
    public class AddProjectCommand : IRequest<int>, IMapFrom<Project>
    {
        [Required] public string Name { get; set; }

        [Required] public string Code { get; set; }

        [Required] public string Division { get; set; }

        [Required] public string KeyAccountManager { get; set; }

        public string DeliveryResponsibleName { get; set; }

        public int ProjectStateTypeId { get; set; }
        public bool DodRequired { get; set; }
        public bool PhrRequired { get; set; }

        public DateTime? PhrRequiredFrom
        {
            get
            {
                if (PhrRequired)
                {
                    return (DateTime.Now);
                }

                return null;
            }
        }

        public bool DmrRequired { get; set; }
        public DateTime? DmrRequiredFrom { get; set; }
        public DateTime? DmrRequiredTo { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }

        public List<(string, string)> UserRoleList { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddProjectCommand, Project>()
                .ConstructUsing(cmd => new Project(0, cmd.Name, cmd.Code, cmd.Division, cmd.KeyAccountManager,
                    cmd.ProjectStartDate, cmd.PhrRequired, cmd.DmrRequired, cmd.DodRequired, cmd.ProjectStateTypeId,
                    cmd.UserRoleList, cmd.DeliveryResponsibleName, null, null, null,
                    null, cmd.ProjectEndDate, cmd.PhrRequiredFrom, cmd.DmrRequiredFrom, cmd.DmrRequiredTo,
                    null, null, null, null, null,
                    null, null, null, null));
        }

        public class Handler : ExecutionHandlerBase<AddProjectCommand, int>
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

            public override async Task<int> HandleAsync(AddProjectCommand request)
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        request.UserRoleList = await _mediator.SendAsync(new GetListUserRoleQuery());

                        var project = _mapper.Map<Project>(request);
                        project.CreatedDate = DateTime.Now;

                        await _dbContext.Projects.AddAsync(project);
                        await _dbContext.SaveChangesAsync();

                        await transaction.CommitAsync();

                        return project.Id;
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }
    }
}