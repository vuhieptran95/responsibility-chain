using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    // base-address of your identityserver
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    // name of the API resource
                    options.ApiName = "api1";
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("fullAccess",
                    builder =>
                    {
                        builder.RequireScope("api1.read_only");
                        builder.RequireScope("api1.full_access");
                    });
                
                // opt.AddPolicy("somehting", builder =>
                // {
                //     builder.RequireScope()
                // });
                //
                // opt.AddPolicy();
                //
                // IAuthorizationRequirement
                // var abc = new AuthorizationPolicy();
            });
            
            services.AddControllers(opt =>
            {
                var policy = ScopePolicy.Create("api1.full_access");
                opt.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}