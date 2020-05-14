// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using IdentityServer.Configs;
using IdentityServer.Features;
using IdentityServer.Features.Business.Clients;
using IdentityServer.Features.Business.ScopeProviders;
using IdentityServer.Features.Business.Users;
using IdentityServer.Features.Domains;
using IdentityServer.Quickstart;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nito.AsyncEx;
using ResponsibilityChain.Business;

namespace IdentityServer
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            var builder = services.AddIdentityServer()
                .AddProfileService<ProfileService>()
                .AddResourceStore<ResourceStore>()
                .AddClientStore<ClientStore>();

            builder.Services.AddSingleton(Config.Ids);
            
            services.AddScoped<IClientStore, ClientStore>();
            services.AddScoped<IResourceStore, ResourceStore>();
            services.AddScoped<IProfileService, ProfileService>();

            var provider = RSAKeys.ImportPrivateKey(File.ReadAllText("private_unencrypted.pem"));
            builder.AddSigningCredential(new RsaSecurityKey(provider),
                IdentityServer4.IdentityServerConstants.RsaSigningAlgorithm.RS256);
            
            services.AddDbContext<IdPDbContext>(
                builder => builder.UseSqlServer(Configuration["ConnectionStrings:IdP"])
            );
            
            services.AddSpaStaticFiles(opt => opt.RootPath = "wwwroot/client-app");
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncomment if you want to add MVC
            app.UseStaticFiles();
            app.Use(async (context, next) =>
            {
                var scope = context.RequestServices.GetService<ILifetimeScope>();
                var customScope = context.RequestServices.GetService<CustomScope>();
                customScope.Scope = scope;

                await next();
            });
            app.UseRouting();

            app.UseCors();
            app.UseIdentityServer();

            

            // uncomment, if you want to add MVC
            app.UseAuthorization();
            
            
            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
            
            app.UseSpa(builder =>
            {
                if (env.IsDevelopment())
                {
                    // builder.UseProxyToSpaDevelopmentServer("http://localhost:8075");
                }
            });
        }
    }
}