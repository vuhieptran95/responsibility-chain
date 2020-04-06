using System;
using System.Collections.Generic;
using ProjectHealthReport.Domains.Domains;

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
        public static void Throw(BusinessError businessError, object domainInstance)
        {
            Throw(businessError, domainInstance, null, null);
        }

        public static void Throw(BusinessError businessError, object domainInstance, object relatedInstance)
        {
            Throw(businessError, domainInstance, relatedInstance, null);
        }

        public static void Throw(BusinessError businessError, object domainInstance, IEnumerable<object> relatedInstances)
        {
            Throw(businessError, domainInstance, null, relatedInstances);
        }

        public static void Throw(BusinessError businessError, object domainInstance, object relatedInstance,
            IEnumerable<object> relatedInstances)
        {
            var error = ErrorCode(businessError);
            if (relatedInstance != null)
            {
                throw new Features.Exceptions.BusinessException(businessError.ToString(), error.Item1, error.Item2, domainInstance,
                    relatedInstance);
            }

            if (relatedInstances != null)
            {
                throw new Features.Exceptions.BusinessException(businessError.ToString(), error.Item1, error.Item2, domainInstance,
                    relatedInstances);
            }

            throw new Features.Exceptions.BusinessException(businessError.ToString(), error.Item1, error.Item2, domainInstance);
        }

        /// <summary>
        /// (message, domainType)
        /// </summary>
        /// <param name="businessError"></param>
        /// <returns>(message, domainType)</returns>
        /// <exception cref="Exception"></exception>
        public static (string, Type) ErrorCode(BusinessError businessError)
        {
            switch (businessError)
            {
                case Exceptions.BusinessError.B001:
                    return ("Project's Code is invalid: maximum 3 characters (number and letter only)",
                        typeof(Project));
                case Exceptions.BusinessError.B002:
                    return ("Project's Division is invalid: incorrect organization information", typeof(Project));
                case Exceptions.BusinessError.B003:
                    return ("Project's Key Account Manager is invalid: incorrect organization information",
                        typeof(Project));
                case Exceptions.BusinessError.B004:
                    return ("Project's Delivery Responsible Name is invalid: incorrect organization information",
                        typeof(Project));
                case Exceptions.BusinessError.B005:
                    return ("Project's Jira link if exists, must be an url", typeof(Project));
                case Exceptions.BusinessError.B006:
                    return ("Project's Source code link if exists, must be an url", typeof(Project));
                case Exceptions.BusinessError.B007:
                    return ("Project's End Date if exists, must bigger than Start Date", typeof(Project));
                case Exceptions.BusinessError.B008:
                    return ("If project's PhrRequired, PhrRequiredFrom must not be null", typeof(Project));
                case Exceptions.BusinessError.B009:
                    return ("If project's DmrRequired, DmrRequiredFrom must not be null", typeof(Project));
                case Exceptions.BusinessError.B010:
                    return (
                        "If both project's DmrRequiredFrom and DmrRequiredTo are specified, To value must be bigger than From value",
                        typeof(Project));
                case Exceptions.BusinessError.B011:
                    return ("If Project's DodRequired, PhrRequired must be true", typeof(Project));
                case Exceptions.BusinessError.B012:
                    return ("If project's not PhrRequired, cannot set PhrRequiredFrom", typeof(Project));
                case Exceptions.BusinessError.B014:
                    return ("Project Access Email is invalid: incorrect organization information",
                        typeof(ProjectAccess));
                case Exceptions.BusinessError.B015: return ("Project Access Role is invalid", typeof(ProjectAccess));
                case Exceptions.BusinessError.B016: return ("DoDReport File must have both link and file name", typeof(DoDReport));
                case Exceptions.BusinessError.B017: return ("If DoDReport File link exists, must be an url", typeof(DoDReport));
                case Exceptions.BusinessError.B018: return ("If Metric ValueType is Number, DodReport Value must be Number", typeof(DoDReport));
                case Exceptions.BusinessError.B019: return ("If Metric Value Type is Select, DodReport Value must be one of Metrics's SelectValues", typeof(DoDReport));
                case Exceptions.BusinessError.B020: return ("If Project's not DodRequired, DodReport cannot be inserted", typeof(DoDReport));

                default: throw new Exception("Invalid error code!");
            }
        }
    }
}