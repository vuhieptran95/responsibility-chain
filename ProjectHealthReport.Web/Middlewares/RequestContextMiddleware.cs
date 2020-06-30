using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
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
                var type = claim.Type;
                switch (claim.Type)
                {
                    case "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier":
                        requestContext.UserId = value;
                        break;
                    case "name":
                        requestContext.Username = value;
                        requestContext.UserEmail = value;
                        break;
                    case "client_id":
                        requestContext.Username = value;
                        requestContext.UserEmail = value;
                        break;
                    case "http://schemas.microsoft.com/ws/2008/06/identity/claims/role":
                        requestContext.UserRole = value;
                        break;
                    case "rights":
                        requestContext.AccessRights =
                            value.Split(' ').Select(i => i.Split('.')[1]).ToList();
                        break;
                    case "scope":
                        requestContext.AccessRights.Add(value.Split('.').Last());
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