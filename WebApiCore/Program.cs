using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microphone;
using Microphone.Consul;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApiCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
            //
            /* Enabling consul.io for service registry
            var options = new ConsulOptions();
            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger("logger");
            var provider = new ConsulProvider(loggerFactory, Options.Create(options));
            Cluster.RegisterService(new Uri($"http://localhost:5000"), provider, "WebAPICore", "v1", logger);
            */
            host.Run();
        }
    }
}
