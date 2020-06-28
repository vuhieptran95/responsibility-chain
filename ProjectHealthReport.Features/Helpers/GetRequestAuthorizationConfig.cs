using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using ProjectHealthReport.Features.Projects.Queries.GetProjects;
using ResponsibilityChain;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Helpers
{
    public class GetRequestAuthorizationConfigs : Request<GetRequestAuthorizationConfigs.Dto>
    {
        public class Handler : IExecution<GetRequestAuthorizationConfigs, Dto>
        {
            public Task HandleAsync(GetRequestAuthorizationConfigs request)
            {
                var interfaceType = typeof(IAuthorizationConfig<GetProjectQuery>);
                var defaultImpl = new DefaultAuthorizationConfig<object>();

                var assembly = Assembly.GetAssembly(typeof(GetDivisionNamesQuery));
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (type.IsAssignableFrom(interfaceType))
                    {
                        var res = (List<(string[] Resources, string[] Actions)>) type
                            .GetMethod(nameof(defaultImpl.GetAccessRights))
                            ?.Invoke(type, default);
                    }
                }

                throw new System.NotImplementedException();
            }
        }

        public class Dto
        {
        }

    }
}