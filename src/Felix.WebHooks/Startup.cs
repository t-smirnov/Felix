using System;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Logging;
using Microsoft.Practices.Unity;
using Owin;
using System.Web.Http;
using MassTransit;
using NLog.Internal;
using NLog.Owin.Logging;
using Unity.WebApi;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Felix.WebHooks
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var http = new HttpConfiguration();

            var rabbitUri = ConfigurationManager.AppSettings["rabbit"];

            var busControl = Bus.Factory.CreateUsingRabbitMq(host =>
            {
                host.Host(new Uri(rabbitUri), config =>
                {
                    config.Username("guest");
                    config.Password("guest");
                });
            });
            app.UseWebApi(http);
            app.UseNLog();
            var container = new UnityContainer();

            container.RegisterInstance(app.GetLoggerFactory().Create("WebHooks"));
            container.RegisterInstance(busControl);


            container.RegisterType<IBusControl>();

            http.DependencyResolver = new UnityDependencyResolver(container);

            http.MapHttpAttributeRoutes();
            http.EnsureInitialized();
        }
    }
}