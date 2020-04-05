using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Helpers
{
    public class GetAllowedWeeksOfYearQuery : IRequest<List<int>>
    {
        public int SelectedYear { get; set; }

        public class Handler : ExecutionHandlerBase<GetAllowedWeeksOfYearQuery, List<int>>
        {
            public override Task<List<int>> HandleAsync(GetAllowedWeeksOfYearQuery request)
            {
                var listWeeks = new List<int>();
                var currentYear = TimeHelper.GetCurrentYearIso();
                if (request.SelectedYear < currentYear)
                {
                    listWeeks = TimeHelper.GetNumberOfWeekInAYear(request.SelectedYear);
                }
                else if (request.SelectedYear == currentYear)
                {
                    var currentWeek = TimeHelper.GetCurrentWeekIso();
                    listWeeks = Enumerable.Range(1, currentWeek).ToList();
                }

                return Task.FromResult(listWeeks);
            }
        }
    }
}