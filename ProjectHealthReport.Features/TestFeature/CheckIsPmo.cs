using System;
using System.Threading.Tasks;
using ResponsibilityChain.Business.Authorizations;

namespace ProjectHealthReport.Features.TestFeature
{
    public class CheckIsPmo : AuthorizationHandler<GetTestFeatureQuery, GetTestFeatureDto>
    {
        public override Task<GetTestFeatureDto> HandleAsync(GetTestFeatureQuery request)
        {
            if (request.Role == "pmo")
            {
                Console.WriteLine("This is PMO");
                return Task.FromResult<GetTestFeatureDto>(default);
            }

            return base.HandleAsync(request);
        }
    }
}