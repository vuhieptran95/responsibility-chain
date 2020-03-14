using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Web.Middlewares
{
    public class RequestContextMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, RequestContext requestContext)
        {
            var claims = context.User.Claims;
            foreach (var claim in claims)
            {
                var value = claim.Value;
                switch (claim.Type)
                {
                    case "client_id":
                        requestContext.UserId = value;
                        break;
                    case "sub":
                        requestContext.UserId = value;
                        break;
                    case "role":
                        requestContext.UserRole = value;
                        break;
                    case "email":
                        requestContext.UserEmail = value;
                        break;
                    case "rights":
                        requestContext.AccessRights =
                            value.Split(' ').Select(i => i.TrimStart("phr.".ToCharArray())).ToList();
                        break;
                    case "scope":
                        requestContext.AccessRights.Add(value.TrimStart("phr.".ToCharArray()));
                        break;
                }
            }
            // var rights = "item1:read item2:read item1:update".Split(' ').ToList();
            // requestContext.AccessRights = rights;

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}