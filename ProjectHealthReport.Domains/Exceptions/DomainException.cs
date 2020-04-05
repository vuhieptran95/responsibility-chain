using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectHealthReport.Domains.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string code, string message, Type domainType, object domainInstance) : base(message)
        {
            Code = code;
            DomainType = domainType;
            DomainInstance = domainInstance;
        }

        public DomainException(string code, string message, Type domainType, object domainInstance, object relatedInstance) 
            : this(code, message, domainType, domainInstance)
        {
            RelatedInstance = relatedInstance;
        }

        public DomainException(string code, string message, Type domainType, object domainInstance, IEnumerable<object> relatedInstances)
            : this(code, message, domainType, domainInstance)
        {
            RelatedInstances = relatedInstances;
        }

        public Type DomainType { get; private set; }
        public string Code { get; private set; }
        public object DomainInstance { get; private set; }
        public object RelatedInstance { get; private set; }
        public IEnumerable<object> RelatedInstances { get; private set; }
    }
}