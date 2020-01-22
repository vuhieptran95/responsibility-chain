using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ResponsibilityChain.WebApi
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
            services.AddScoped(typeof(IThing<,>), typeof(GenericThing<,>));
            services.AddScoped(typeof(Handler<,>), typeof(Handler<,>));
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }

    public class BusinessHandler<IRequest, IResponse> : Handler<Request1, Response1>
    {
        public override Task<Response1> HandleAsync(Request1 request1)
        {
            return Task.FromResult(new Response1());
        }
    }

    public interface IRequest<TResponse>
    {
        
    }

    public class Request1 : IRequest<Response1>
    {
        
    }

    public interface IResponse
    {
        
    }

    public class Response1
    {
        public Response1()
        {
            Type = "newly created";
        }
        public string Type { get; set; }
    }
    
    public interface IThing<T1, T2>
    {
        string GetName { get; }
    }
    
    public class GenericThing<T1, T2> : IThing<T1, T2>
    {
        public GenericThing()
        {
            GetName = typeof(T1).Name + " " + typeof(T2).Name;
        }

        public string GetName { get; }
    }
}