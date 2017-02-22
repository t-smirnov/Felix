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
            
            var container = new UnityContainer();
            var logger = new NLogFactory().Create("Startup");

            try
            {
                
                var rabbitUri = ConfigurationManager.AppSettings["rabbit"];
                logger.WriteInformation($"## Trying to resolve rabbit at {rabbitUri}");

                var busControl = Bus.Factory.CreateUsingRabbitMq(host =>
                {
                    host.Host(new Uri(rabbitUri), config =>
                    {
                        config.Username("guest");
                        config.Password("guest");
                    });
                });
                container.RegisterInstance(busControl);
            }
            catch (Exception e)
            {
                logger.WriteError(e.Message);
            }


            app.UseWebApi(http);
            app.UseNLog();

            container.RegisterInstance(app.GetLoggerFactory().Create("WebHooks"));



            container.RegisterType<IBusControl>();

            http.DependencyResolver = new UnityDependencyResolver(container);

            http.MapHttpAttributeRoutes();
            http.EnsureInitialized();
        }
    }
}