using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using MassTransit;
using Microsoft.Practices.Unity;

namespace Felix.WebHooks.Extensions
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseMassTransit(this IAppBuilder app, string rabbitUri, IUnityContainer container)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(host =>
            {
                host.Host(new Uri(rabbitUri), config =>
                {
                    config.Username("guest");
                    config.Password("guest");
                });
            });
            container.RegisterInstance(busControl);
            return app;
        }
    }
}