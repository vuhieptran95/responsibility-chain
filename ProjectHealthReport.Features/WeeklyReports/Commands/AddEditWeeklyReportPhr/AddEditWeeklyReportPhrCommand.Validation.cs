using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Features.Exceptions;
using ResponsibilityChain.Business.Validations;

namespace ProjectHealthReport.Features.WeeklyReports.Commands.AddEditWeeklyReportPhr
{
    public partial class AddEditWeeklyReportPhrCommand
    {
        public class EnforceSubmitTime : IPreValidation<AddEditWeeklyReportPhrCommand, int>
        {
            private readonly IOptions<AuthorizationRules> _rules;

            public EnforceSubmitTime(IOptions<AuthorizationRules> rules)
            {
                _rules = rules;
            }

            public Task HandleAsync(AddEditWeeklyReportPhrCommand request)
            {
                var rule = _rules.Value.PMsCanOnlyEditTheirReportsTill;

                if (!rule.IsEnabled)
                {
                    return Task.CompletedTask;
                }

                if (request.RequestContext.AccessRights.Contains(AuthorizationHelper.ScopeUpdatePast) &&
                    request.RequestContext.AccessRights.Contains(AuthorizationHelper.ScopeCreatePast))
                {
                    return Task.CompletedTask;
                }

                var isoDay = TimeHelper.GetIsoDayOfWeek(rule.Day);
                var maxAllowedDateTime = TimeHelper.GetDateTimeOfXHourYDayNextZWeekFollowingYear(rule.Hour, isoDay, 1,
                    request.Report.SelectedYear, request.Report.SelectedWeek);

                if (maxAllowedDateTime < DateTime.Now)
                {
                    BusinessExceptionCode.Throw(BusinessError.B001, request);
                }
                
                return Task.CompletedTask;
            }
        }
    }
}