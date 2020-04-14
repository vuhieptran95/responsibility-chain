﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Helpers
{
    public class GetAllowedYearWeeksQuery: IRequest<List<int>>
    {
        public int FromYear { get; set; }

        public class Handler : ExecutionHandlerBase<GetAllowedYearWeeksQuery, List<int>>
        {
            public override Task HandleAsync(GetAllowedYearWeeksQuery request)
            {
                
                var currentYear = TimeHelper.GetCurrentYearIso();
                var listYearWeeks = new List<int>();
                if (request.FromYear > currentYear)
                {
                    return Task.FromResult(listYearWeeks);
                }

                var currentWeek = TimeHelper.GetCurrentWeekIso();
                listYearWeeks = Enumerable.Range(1, currentWeek)
                    .Select(w => TimeHelper.CalculateYearWeek(currentYear, w)).ToList();

                for (int i = request.FromYear; i < currentYear; i++)
                {
                    var yearweeksOfFromYear = TimeHelper.GetNumberOfWeekInAYear(request.FromYear)
                        .Select(w => TimeHelper.CalculateYearWeek(request.FromYear, w)).ToList();
                    yearweeksOfFromYear.AddRange(listYearWeeks);
                    listYearWeeks = yearweeksOfFromYear;
                }

                listYearWeeks = listYearWeeks.OrderByDescending(yw => yw).ToList();
            
                request.Response = listYearWeeks;

                return Task.CompletedTask;
            }
        }

        public List<int> Response { get; set; }
    }
}