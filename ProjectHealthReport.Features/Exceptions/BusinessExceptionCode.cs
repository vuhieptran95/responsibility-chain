using System;
using System.Collections.Generic;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Features.WeeklyReports.Commands.AddEditWeeklyReportPhr;

namespace ProjectHealthReport.Features.Exceptions
{
    public enum BusinessError
    {
        B001,
        B002,
        B003,
        B004,
        B005,
        B006,
        B007,
        B008,
        B009,
        B010,
        B011,
        B012,
        B013,
        B014,
        B015,
        B016,
        B017,
        B018,
        B019,
        B020,
    }

    public class BusinessExceptionCode
    {
        public static void Throw(BusinessError businessError, object request)
        {
            var error = ErrorCode(businessError);

            throw new Features.Exceptions.BusinessException(businessError.ToString(), error.Message, error.Type, request);
        }

        /// <summary>
        /// (message, domainType)
        /// </summary>
        /// <param name="businessError"></param>
        /// <returns>(message, domainType)</returns>
        /// <exception cref="Exception"></exception>
        public static (string Message, Type Type) ErrorCode(BusinessError businessError)
        {
            switch (businessError)
            {
                case Exceptions.BusinessError.B001:
                    return ("Cannot Add/Edit weekly report after locked time",
                        typeof(AddEditWeeklyReportPhrCommand));

                default: throw new Exception("Invalid error code!");
            }
        }
    }
}