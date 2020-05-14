using System;
using System.Linq;
using System.Threading.Tasks;
using ResponsibilityChain.Business.Validations;

namespace ProjectHealthReport.Features.DoDs.AddEditDoDReport
{
    public partial class EditDoDReportCommand
    {
        public class EditOneProjectAtATime : IPreValidation<EditDoDReportCommand, int>
        {
            public Task HandleAsync(EditDoDReportCommand request)
            {
                if (request.DodReports.Count > 0)
                {
                    var projectId = request.DodReports[0].ProjectId;
                    if (request.DodReports.Any(d => d.ProjectId != projectId))
                    {
                        throw new Exception("Edit 1 project at a time");
                    }
                }

                return Task.CompletedTask;
            }
        }
    }
}