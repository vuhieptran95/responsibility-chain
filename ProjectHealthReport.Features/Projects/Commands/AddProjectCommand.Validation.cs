using System.Linq;
using System.Threading.Tasks;
using ProjectHealthReport.Features.Exceptions;
using ProjectHealthReport.Features.Projects.Queries.GetProjectsCaching;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.Validations;

namespace ProjectHealthReport.Features.Projects.Commands
{
    public partial class AddProjectCommand
    {
        public class ProjectCodeMustBeUnique : IPreValidation<AddProjectCommand, int>
        {
            private readonly IMediator _mediator;

            public ProjectCodeMustBeUnique(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task HandleAsync(AddProjectCommand request)
            {
                var projects = (await _mediator.SendAsync(new GetProjectsCachingQuery())).Projects;

                var existed = projects.FirstOrDefault(p => p.Code == request.Code);

                if (existed != null)
                {
                    BusinessExceptionCode.Throw(BusinessError.B002, request);
                }
            }
        }
    }
}