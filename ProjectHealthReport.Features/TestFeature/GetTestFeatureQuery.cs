using System.Threading.Tasks;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.TestFeature
{
    public class GetTestFeatureQuery : IRequest<GetTestFeatureDto>
    {
        
    }

    public class GetTestFeatureQueryHandler : ExecutionHandler<GetTestFeatureQuery, GetTestFeatureDto>
    {
        public override Task<GetTestFeatureDto> HandleAsync(GetTestFeatureQuery request)
        {
            return Task.FromResult(new GetTestFeatureDto()
            {
                Name = "hiepdeptrai"
            });
        }
    }

    public class GetTestFeatureDto
    {
        public string Name { get; set; }
    }
}