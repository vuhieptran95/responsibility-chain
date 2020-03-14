using System;
using System.Threading.Tasks;
using ResponsibilityChain;
using ResponsibilityChain.Business.Authorizations;

namespace ProjectHealthReport.Features.TestFeature
{
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

        public class CheckIsCooPeter : BranchHandler<GetTestFeatureQuery, GetTestFeatureDto>
        {
            public CheckIsCooPeter(CheckIsCooPeter1 peter1, CheckIsCooPeter2 peter2)
            {
                AddBranchHandler(peter1);
                AddBranchHandler(peter2);
            }

            public override async Task<GetTestFeatureDto> HandleAsync(GetTestFeatureQuery request)
            {
                if (request.Name == "Peter")
                {
                    Console.WriteLine("This is COO Peter");
                    return await Branch.HandleAsync(request);
                }

                return await Next.HandleAsync(request);
            }

            public class CheckIsCooPeter1 : BranchHandler<GetTestFeatureQuery, GetTestFeatureDto>
            {
                public override Task<GetTestFeatureDto> HandleAsync(GetTestFeatureQuery request)
                {
                    if (request.Order == 1)
                    {
                        Console.WriteLine("This is COO Peter 1");
                        return Task.FromResult<GetTestFeatureDto>(default);
                    }

                    return base.HandleAsync(request);
                }
            }

            public class CheckIsCooPeter2 : BranchHandler<GetTestFeatureQuery, GetTestFeatureDto>
            {
                public override Task<GetTestFeatureDto> HandleAsync(GetTestFeatureQuery request)
                {
                    if (request.Order == 2)
                    {
                        Console.WriteLine("This is COO Peter 2");
                        return Task.FromResult<GetTestFeatureDto>(default);
                    }

                    return base.HandleAsync(request);
                }
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
    }
}