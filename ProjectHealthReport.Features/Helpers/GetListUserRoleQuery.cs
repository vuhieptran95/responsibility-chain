using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Helpers
{
    public class GetListUserRoleQuery : IRequest<List<(string Email, string Name)>>
    {
        public class Handler: IExecution<GetListUserRoleQuery, List<(string Email, string Name)>>
        {
            public async Task HandleAsync(GetListUserRoleQuery request)
            {
                await Task.Delay(2000);
                request.Response = (AuthorizationHelper.UserRoleList);
            }
        }
        
        public class CacheConfig: ICacheConfig<GetListUserRoleQuery>
        {
            public bool IsCacheEnabled { get; } = true;
            public DateTimeOffset CacheDateTimeOffset { get; } = DateTimeOffset.Now.AddDays(1);

            public string GetCacheKey(GetListUserRoleQuery request)
            {
                return request.GetType().FullName;
            }
        }

        public List<(string, string)> Response { get; set; }
    }
}