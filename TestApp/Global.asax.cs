using Autofac;
using Autofac.Integration.WebApi;
using Steeltoe.Discovery.Client;
using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TestApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private IDiscoveryClient _client;
        protected void Application_Start()
        {
            var config = GlobalConfiguration.Configuration;
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

                    
            // Build application configuration
            ConfigServerConfig.RegisterConfig("test");

            // Create IOC container builder
            var builder = new ContainerBuilder();
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register IDiscoveryClient, etc.
            builder.RegisterDiscoveryClient(ConfigServerConfig.Configuration);
            

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);


            // Start the Discovery client background thread
            _client = container.Resolve<IDiscoveryClient>();
            

        }
        protected void Application_End()
        {
            // Unregister current app with Service Discovery server
            _client.ShutdownAsync();
        }
    }
}
