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
using Felix.WebHooks.Extensions;

namespace Felix.WebHooks
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var http = new HttpConfiguration();

            var container = new UnityContainer();
            var logger = new NLogFactory().Create("Startup");

            var rabbitUri = ConfigurationManager.AppSettings["rabbit"];
            logger.WriteInformation($"## Trying to resolve rabbit at {rabbitUri}");

            app.UseWebApi(http);
            app.UseNLog();

            container.RegisterInstance(app.GetLoggerFactory().Create("WebHooks"));

#if RElEASE
            app.UseMassTransit("rabbitmq://cloundwin.westeurope.cloudapp.azure.com:5672");
#endif
#if DEBUG
            app.UseMassTransit("rabbitmq://localhost:5672", container);

#endif





            http.DependencyResolver = new UnityDependencyResolver(container);

            http.MapHttpAttributeRoutes();
            http.EnsureInitialized();
        }
    }
}