using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
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
    public class AddProjectCommand : IRequest<int>, IMapTo<ProjectProxy>
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

        public class Handler : ExecutionHandler<AddProjectCommand, int>
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

            public override async Task HandleAsync(AddProjectCommand request)
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var userRoleList = await _mediator.SendAsync(new GetListUserRoleQuery());

                        var project = _mapper.Map<ProjectProxy>(request);
                        project.CreatedDate = DateTime.Now;

                        await _dbContext.Projects
                            .AddAsync(_mapper.Map<Project>(project, opts =>
                            {
                                opts.Items[MiscHelper.UserRoleListCtor] = userRoleList;
                            }));
                        await _dbContext.SaveChangesAsync();

                        await transaction.CommitAsync();

                        request.Response = project.Id;
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        public int Response { get; set; }
    }
}