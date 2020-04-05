using System.Collections.Generic;

namespace ProjectHealthReport.Domains.Domains
{
    public interface IValidateDomain
    {
        void ValidateDomain();
    }
    
    public static class ValidateDomainExtension
    {
        public static void Validate(this IValidateDomain domain)
        {
            domain.ValidateDomain();
        }

        public static void Validate(this IEnumerable<IValidateDomain> domains)
        {
            foreach (var domain in domains)
            {
                domain.ValidateDomain();
            }
        }
    }
}