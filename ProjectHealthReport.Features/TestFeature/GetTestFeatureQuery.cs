using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ResponsibilityChain;
using ResponsibilityChain.Business.Authorizations;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.TestFeature
{
    public class GetTestFeatureQuery : IRequest<GetTestFeatureDto>
    {
        public string Role { get; set; } = "coo";
        public string Name { get; set; } = "Peter";
    }

    public class CheckIsCoo : AuthorizationHandler<GetTestFeatureQuery, GetTestFeatureDto>
    {
        public CheckIsCoo(
            CheckIsCooPele checkIsCooPele,
            CheckIsCooPeter checkIsCooPeter)
        {
            AddBranchHandler(checkIsCooPeter);
            AddBranchHandler(checkIsCooPele);
        }

        public override async Task<GetTestFeatureDto> HandleAsync(GetTestFeatureQuery request)
        {
            if (request.Role == "coo")
            {
                Console.WriteLine("This is COO");
                return await Branch.HandleAsync(request);
            }

            return await Next.HandleAsync(request);
        }
    }

    public class CheckIsCooPeter : BranchHandler<GetTestFeatureQuery, GetTestFeatureDto>
    {
        public override Task<GetTestFeatureDto> HandleAsync(GetTestFeatureQuery request)
        {
            if (request.Name == "Peter")
            {
                Console.WriteLine("This is COO Peter");
                return Task.FromResult<GetTestFeatureDto>(default);
            }

            return base.HandleAsync(request);
        }
    }

    public class CheckIsCooPele : BranchHandler<GetTestFeatureQuery, GetTestFeatureDto>
    {
        public override Task<GetTestFeatureDto> HandleAsync(GetTestFeatureQuery request)
        {
            if (request.Name == "Pele")
            {
                Console.WriteLine("This is COO Pele");
                return Task.FromResult<GetTestFeatureDto>(default);
            }

            return base.HandleAsync(request);
        }
    }

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