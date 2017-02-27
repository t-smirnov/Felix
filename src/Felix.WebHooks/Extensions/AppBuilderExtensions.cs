using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using AlchemyAPIClient;
using Felix.Core.Interfaces;
using Owin;
using MassTransit;
using Microsoft.Owin.Logging;
using Microsoft.Practices.Unity;
using Telegram.Bot;
using Unity.WebApi;

namespace Felix.WebHooks.Extensions
{
    public static class AppBuilderExtensions
    {
        private static IAppBuilder UseMassTransit(this IAppBuilder app, string rabbitUri, IUnityContainer container)
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


        public static IAppBuilder UseContainer(this IAppBuilder app, HttpConfiguration config)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (config == null) throw new ArgumentNullException(nameof(config));

            var rabbitUri = ConfigurationManager.AppSettings["rabbit"];
            var token = ConfigurationManager.AppSettings["token"];
            var client = new AlchemyClient("ab5ff507c8d6e3e7d4019b4e81451a6b0bf147c0", "https://gateway-a.watsonplatform.net/calls");

            var container = new UnityContainer();
            container.RegisterInstance(client);
            container.RegisterType<ITelegramBotClient, TelegramBotClient>(new ContainerControlledLifetimeManager(), new InjectionConstructor(token));
            container.RegisterInstance(app.GetLoggerFactory().Create("WebHooks"));
            container.RegisterType<IBot, GenerativeBot>();
            app.UseMassTransit(rabbitUri, container);

            
            config.DependencyResolver = new UnityDependencyResolver(container);
            return app;
        }
    }
}