using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace WebApiCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //app.UseMvc();
            var apiRouteHandler = new RouteHandler(context =>
            {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync(
                    $"Hello! Route values: {string.Join(", ", routeValues)}");
            });

            var routeBuilder = new RouteBuilder(app, apiRouteHandler);
            routeBuilder.MapRoute(
                "Default API Route",
                "api/{controller}/{operation}");
            routeBuilder.MapRoute(
                "API Route",
                "api/{controller}/{operation}/{id:int}");

            routeBuilder.MapGet("status", context =>
            {
               return context.Response.WriteAsync($"OK!");
            });
            //app.Map("/status", HandleBranch);
            var routes = routeBuilder.Build();
            app.UseRouter(routes);
            app.UseMvcWithDefaultRoute();
        }
        private static void HandleBranch(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"OK");
            });
        }
    }
}
