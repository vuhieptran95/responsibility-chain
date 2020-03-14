using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ResponsibilityChain;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.TestFeature
{
    public class GetTestFeatureQuery : IRequest<GetTestFeatureDto>
    {
        public string Role { get; set; } = "coo";
        public string Name { get; set; } = "Peter";
        public int Order { get; set; } = 1;
        
        public class AuthorizationConfig: AuthorizationConfig<GetTestFeatureQuery, GetTestFeatureDto>
        {
            public AuthorizationConfig(RequestContext requestContext) : base(requestContext)
            {
                AccessRights.Add(("item1 item2", "read"));
                AccessRights.Add(("item2", "update"));
            }
        }
    }
    
    

    public class GetTestFeatureHandlerBase : ExecutionHandlerBase<GetTestFeatureQuery, GetTestFeatureDto>
    {
        public GetTestFeatureHandlerBase()
        {
        }
        public override Task<GetTestFeatureDto> HandleAsync(GetTestFeatureQuery request)
        {
            var response=(new GetTestFeatureDto()
            {
                Name = "hiepdeptrai"
            });
            
            Console.WriteLine("Execute request");

            // return base.HandleAsync(request);
            return Task.FromResult(response) ;
        }
    }

    public class GetTestFeatureDto
    {
        public string Name { get; set; }
    }
}