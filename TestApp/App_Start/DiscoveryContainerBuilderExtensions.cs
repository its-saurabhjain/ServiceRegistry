using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Pivotal.Discovery.Client;
using Steeltoe.CloudFoundry.Connector;
using Steeltoe.CloudFoundry.Connector.Services;
using System;

namespace TestApp
{
    public static class DiscoveryContainerBuilderExtensions
    {
        public static void RegisterDiscoveryClient(this ContainerBuilder container, IConfigurationRoot config)
        {
            EurekaServiceInfo info = config.GetSingletonServiceInfo<EurekaServiceInfo>();
            DiscoveryOptions configOptions = new DiscoveryOptions(config)
            {
                ClientType = DiscoveryClientType.EUREKA
            };

            DiscoveryClientFactory factory = new DiscoveryClientFactory(info, configOptions);
            container.Register<IDiscoveryClient>(c => (IDiscoveryClient)factory.CreateClient()).SingleInstance();
        }

    }
}