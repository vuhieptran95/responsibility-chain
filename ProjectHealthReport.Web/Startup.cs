using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Features;
using ProjectHealthReport.Web.Middlewares;
using ResponsibilityChain.Business;

namespace ProjectHealthReport.Web
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
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "phr";
                })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.ClientId = "mvc";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";
                    options.UseTokenLifetime = true;
                    // options.SaveTokens = true;
                    
                    options.Events.OnRedirectToIdentityProvider = context =>
                    {
                        if (context.Request.Path.StartsWithSegments("/api") && context.Options.AuthenticationMethod == OpenIdConnectRedirectBehavior.RedirectGet)
                        {
                            if (!context.Request.Query.TryGetValue("redirectUrlIdP", out var redirectUrlIdP))
                            {
                                context.Response.StatusCode = 401;
                                context.HandleResponse();
                                
                                return Task.CompletedTask;
                            }
                            
                            var properties = context.Properties;
                            properties.Items[".redirect"] = Encoding.UTF8.GetString(Convert.FromBase64String(redirectUrlIdP));
                            
                            var message = context.ProtocolMessage;

                            if (!string.IsNullOrEmpty(message.State))
                            {
                                properties.Items[OpenIdConnectDefaults.UserstatePropertiesKey] = message.State;
                            }

                            // When redeeming a 'code' for an AccessToken, this value is needed
                            properties.Items.Add(OpenIdConnectDefaults.RedirectUriForCodePropertiesKey, message.RedirectUri);

                            message.State = context.Options.StateDataFormat.Protect(properties);

                            if (string.IsNullOrEmpty(message.IssuerAddress))
                            {
                                throw new InvalidOperationException(
                                    "Cannot redirect to the authorization endpoint, the configuration may be missing or invalid.");
                            }
                            
                            var redirectUri = message.CreateAuthenticationRequestUrl();
                            if (!Uri.IsWellFormedUriString(redirectUri, UriKind.Absolute))
                            {
                                throw new Exception($"The redirect URI is not well-formed. The URI is: '{redirectUri}'.");
                            }

                            var redirect = JsonConvert.SerializeObject(new {redirectUri});

                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            context.Response.WriteAsync(redirect, Encoding.UTF8);
                            
                            context.HandleResponse();
                        }
                        
                        return Task.CompletedTask;
                    };

                    options.Scope.Add("openid");
                    options.Scope.Add("email");
                    options.Scope.Add("rights");
                    options.Scope.Add("role");
                })
                ;

            services.Configure<AuthorizationRules>(Configuration.GetSection("AuthorizationRules"));
            services.Configure<BusinessRules>(Configuration.GetSection("BusinessRules"));

            services.AddDbContext<ReportDbContext>(
                builder => builder.UseSqlServer(Configuration["ConnectionStrings:ProjectHealthReport"])
            );

            services.AddAutoMapper(Assembly.GetAssembly(typeof(Project)), Assembly.GetAssembly(typeof(AutofacModule)));

            // services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllersWithViews();

            services.AddSpaStaticFiles(opt => opt.RootPath = "wwwroot/client-app");
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<RequestContextMiddleware>();

            app.Use(async (context, next) =>
            {
                var scope = context.RequestServices.GetService<ILifetimeScope>();
                var customScope = context.RequestServices.GetService<CustomScope>();
                customScope.Scope = scope;

                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSpa(builder =>
            {
                if (env.IsDevelopment())
                {
                    // builder.UseProxyToSpaDevelopmentServer("http://localhost:8085");
                }
            });
        }
    }
}