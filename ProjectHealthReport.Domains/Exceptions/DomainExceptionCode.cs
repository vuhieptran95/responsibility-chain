using System;
using System.Collections.Generic;
using ProjectHealthReport.Domains.Domains;

namespace ProjectHealthReport.Domains.Exceptions
{
    public enum ErrorCode
    {
        D001,D002,D003,D004,D005,D006,D007,D008,D009,D010,D011,D012,D013
    }
    
    public class DomainExceptionCode
    {
        public static void Throw(ErrorCode errorCode, object domainInstance)
        {
            Throw(errorCode, domainInstance, null, null);
        }
        
        public static void Throw(ErrorCode errorCode, object domainInstance, object relatedInstance)
        {
            Throw(errorCode, domainInstance, relatedInstance, null);
        }
        
        public static void Throw(ErrorCode errorCode, object domainInstance, IEnumerable<object> relatedInstances)
        {
            Throw(errorCode, domainInstance, null, relatedInstances);
        }
        
        public static void Throw(ErrorCode errorCode, object domainInstance, object relatedInstance, IEnumerable<object> relatedInstances)
        {
            var error = ErrorCode(errorCode);
            if (relatedInstance != null)
            {
                throw new DomainException(errorCode.ToString(), error.Item1, error.Item2, domainInstance, relatedInstance);
            }

            if (relatedInstances != null)
            {
                throw new DomainException(errorCode.ToString(), error.Item1, error.Item2, domainInstance, relatedInstances);
            }
            
            throw new DomainException(errorCode.ToString(), error.Item1, error.Item2, domainInstance);
        }
        
        /// <summary>
        /// (message, domainType)
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns>(message, domainType)</returns>
        /// <exception cref="Exception"></exception>
        public static (string, Type) ErrorCode(ErrorCode errorCode)
        {
            switch (errorCode)
            {
                case Exceptions.ErrorCode.D001:
                    return ("Project's Code is invalid: maximum 3 characters (number and letter only)",
                        typeof(Project));
                case Exceptions.ErrorCode.D002:
                    return ("Project's Division is invalid: incorrect organization information", typeof(Project));
                case Exceptions.ErrorCode.D003:
                    return ("Project's Key Account Manager is invalid: incorrect organization information",
                        typeof(Project));
                case Exceptions.ErrorCode.D004:
                    return ("Project's Delivery Responsible Name is invalid: incorrect organization information",
                        typeof(Project));
                case Exceptions.ErrorCode.D005: return ("Project's Jira link if exists, must be an url", typeof(Project));
                case Exceptions.ErrorCode.D006: return ("Project's Source code link if exists, must be an url", typeof(Project));
                case Exceptions.ErrorCode.D007: return ("Project's End Date if exists, must bigger than Start Date", typeof(Project));
                case Exceptions.ErrorCode.D008: return ("If project's PhrRequired, PhrRequiredFrom must not be null", typeof(Project));
                case Exceptions.ErrorCode.D009: return ("If project's DmrRequired, DmrRequiredFrom must not be null", typeof(Project));
                case Exceptions.ErrorCode.D010:
                    return (
                        "If both project's DmrRequiredFrom and DmrRequiredTo are specified, To value must be bigger than From value",
                        typeof(Project));
                case Exceptions.ErrorCode.D011: return ("If Project's DodRequired, PhrRequired must be true", typeof(Project));
                case Exceptions.ErrorCode.D012: return ("If project's not PhrRequired, cannot set PhrRequiredFrom", typeof(Project));
                case Exceptions.ErrorCode.D013: return ("If project's PhrRequired, Delivery Responsible Name must not be null", typeof(Project));

                default: throw new Exception("Invalid error code!");
            }
        }
    }
}