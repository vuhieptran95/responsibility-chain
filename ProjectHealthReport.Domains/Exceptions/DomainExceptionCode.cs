﻿using System;
using System.Collections.Generic;
using ProjectHealthReport.Domains.Domains;

namespace ProjectHealthReport.Domains.Exceptions
{
    public enum DomainError
    {
        D001,
        D002,
        D003,
        D004,
        D005,
        D006,
        D007,
        D008,
        D009,
        D010,
        D011,
        D012,
        D013,
        D014,
        D015,
        D016,
        D017,
        D018,
        D019,
        D020,
        D022,
        D023,
        D024,
        D025,
        D026,
        D027,
        D028,
        D029,
        D030,
        D032,
        D033,
        D034,
        D035,
        D036,
        D037,
        D038,
        D039,
    }

    public class DomainExceptionCode
    {
        #region Throw Exception

        public static void Throw(DomainError domainError, object domainInstance)
        {
            Throw(domainError, domainInstance, null, null);
        }

        public static void Throw(DomainError domainError, object domainInstance, object relatedInstance)
        {
            Throw(domainError, domainInstance, relatedInstance, null);
        }

        public static void Throw(DomainError domainError, object domainInstance, IEnumerable<object> relatedInstances)
        {
            Throw(domainError, domainInstance, null, relatedInstances);
        }

        public static void Throw(DomainError domainError, object domainInstance, object relatedInstance,
            IEnumerable<object> relatedInstances)
        {
            var error = ErrorCode(domainError);
            if (relatedInstance != null)
            {
                throw new DomainException(domainError.ToString(), error.Message, error.Type, domainInstance,
                    relatedInstance);
            }

            if (relatedInstances != null)
            {
                throw new DomainException(domainError.ToString(), error.Message, error.Type, domainInstance,
                    relatedInstances);
            }

            throw new DomainException(domainError.ToString(), error.Message, error.Type, domainInstance);
        }

        #endregion

        /// <summary>
        /// (message, domainType)
        /// </summary>
        /// <param name="domainError"></param>
        /// <returns>(message, domainType)</returns>
        /// <exception cref="Exception"></exception>
        public static (string Message, Type Type) ErrorCode(DomainError domainError)
        {
            switch (domainError)
            {
                case Exceptions.DomainError.D001:
                    return ("Project's Code is invalid: maximum 3 characters (numbers and capital letters only)",
                        typeof(Project));
                case Exceptions.DomainError.D002:
                    return ("Project's Division is invalid: incorrect organization information", typeof(Project));
                case Exceptions.DomainError.D003:
                    return ("Project's Key Account Manager is invalid: incorrect organization information",
                        typeof(Project));
                case Exceptions.DomainError.D004:
                    return ("Project's Delivery Responsible Name is invalid: incorrect organization information",
                        typeof(Project));
                case Exceptions.DomainError.D005:
                    return ("Project's Jira link if exists, must be an url", typeof(Project));
                case Exceptions.DomainError.D006:
                    return ("Project's Source code link if exists, must be an url", typeof(Project));
                case Exceptions.DomainError.D007:
                    return ("Project's End Date if exists, must bigger than Start Date", typeof(Project));
                case Exceptions.DomainError.D008:
                    return ("If project's PhrRequired, PhrRequiredFrom must not be null", typeof(Project));
                case Exceptions.DomainError.D009:
                    return ("If project's DmrRequired, DmrRequiredFrom must not be null", typeof(Project));
                case Exceptions.DomainError.D010:
                    return (
                        "If both project's DmrRequiredFrom and DmrRequiredTo are specified, To value must be bigger than From value",
                        typeof(Project));
                case Exceptions.DomainError.D011:
                    return ("If Project's DodRequired, PhrRequired must be true", typeof(Project));
                case Exceptions.DomainError.D012:
                    return ("If project's not PhrRequired, cannot set PhrRequiredFrom", typeof(Project));
                case Exceptions.DomainError.D013:
                    return ("If project's PhrRequired, Delivery Responsible Name must not be null", typeof(Project));
                case Exceptions.DomainError.D014:
                    return ("Project Access Email is invalid: incorrect organization information",
                        typeof(ProjectAccess));
                case Exceptions.DomainError.D015: return ("Project Access Role is invalid", typeof(ProjectAccess));
                case Exceptions.DomainError.D016:
                    return ("DoDReport File must have both link and file name", typeof(DoDReport));
                case Exceptions.DomainError.D017:
                    return ("If DoDReport File link exists, must be an url", typeof(DoDReport));
                case Exceptions.DomainError.D018:
                    return ("If Metric ValueType is Number, DodReport Value must be Number", typeof(DoDReport));
                case Exceptions.DomainError.D019:
                    return ("If Metric Value Type is Select, DodReport Value must be one of Metrics's SelectValues",
                        typeof(DoDReport));
                case Exceptions.DomainError.D020:
                    return ("If Project's not DodRequired, DodReport cannot be inserted", typeof(DoDReport));

                case Exceptions.DomainError.D022:
                    return ("Metrics can only add new thresholds or edit its own thresholds", typeof(Metric));
                case Exceptions.DomainError.D023:
                    return ("Individual thresholds of a metric must belong to different metric statuses",
                        typeof(Metric));
                case Exceptions.DomainError.D024:
                    return ("ValueType must be Text, Number or Select", typeof(Metric));
                case Exceptions.DomainError.D025:
                    return ("One metric can only have 3 thresholds", typeof(Metric));
                case Exceptions.DomainError.D026:
                    return ("If metric value type is Number, threshold must be in range", typeof(Metric));
                case Exceptions.DomainError.D027:
                    return ("If metric value type is Text, threshold cannot be in range", typeof(Metric));
                case Exceptions.DomainError.D028:
                    return ("If metric value type is Select, threshold cannot be in range", typeof(Metric));
                case Exceptions.DomainError.D029:
                    return ("If Metric Value Type is Select, Select Values must have values", typeof(Metric));

                case Exceptions.DomainError.D030:
                    return ("If threshold is in range, Value must not be specified", typeof(Threshold));
                
                case Exceptions.DomainError.D032:
                    return ("If threshold is in range, threshold bounds and operators must be specified", typeof(Threshold));
                case Exceptions.DomainError.D033:
                    return ("Invalid operators", typeof(Threshold));
                
                case Exceptions.DomainError.D034:
                    return ("DoDReport must have metric", typeof(DoDReport));
                case Exceptions.DomainError.D035:
                    return ("DoDReport must have project", typeof(DoDReport));
                
                case Exceptions.DomainError.D036:
                    return ("Items remaining must be greater than or equal 0", typeof(Project));
                
                case Exceptions.DomainError.D037:
                    return ("Story points remaining must be greater than or equal 0", typeof(Project));
                
                case Exceptions.DomainError.D038:
                    return ("Bugs remaining must be greater than or equal 0", typeof(Project));
                
                case Exceptions.DomainError.D039:
                    return ("Re open bugs must be smaller than or equal total bugs", typeof(Project));


                default: throw new Exception("Invalid error code!");
            }
        }
    }
}