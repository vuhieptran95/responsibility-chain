using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResponsibilityChain.Business.RequestContexts;

namespace ResponsibilityChain.Business.AuthorizationConfigs
{
    public class AuthorizationConfig<TRequest, TResponse> : Handler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        protected readonly RequestContext RequestContext;

        public AuthorizationConfig(RequestContext requestContext)
        {
            AccessRights = new List<(string, string)>();
            RequestContext = requestContext;
            // AccessRights.Add(("item1 item2", "read create"));
            // AccessRights.Add(("item3 item2", "edit"));
            // AccessRights.Add(("item4", "*"));
        }

        public override Task HandleAsync(TRequest request)
        {
            var accessRights = CalculateRights(AccessRights);

            var violatedRights = accessRights.Except(RequestContext.AccessRights).ToList();

            if (violatedRights.Count > 0)
            {
                var violated = string.Join(' ', violatedRights);
                throw new UnauthorizedAccessException($"{violated} right(s) are not permitted for this user");
            }

            return base.HandleAsync(request);
        }

        public List<(string, string)> AccessRights { get; set; }

        public List<string> CalculateRights(List<(string, string)> rights)
        {
            var listRight = new List<string>();
            if (rights == null)
            {
                return listRight;
            }

            foreach (var right in rights)
            {
                var resources = right.Item1
                    .Split(' ').Where(i => !string.IsNullOrWhiteSpace(i)).Distinct().ToList();
                var actions = right.Item2
                    .Split(' ').Where(i => !string.IsNullOrWhiteSpace(i)).Distinct().ToList();
                if (resources.Contains("*"))
                {
                    resources = new List<string> {"read", "create", "delete", "update"};
                }

                var cartesianProducts = actions
                    .SelectMany(action => resources, (action, resource) => resource + ":" + action);

                listRight.AddRange(cartesianProducts);
            }

            return listRight;
        }
    }
}