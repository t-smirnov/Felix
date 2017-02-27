using System;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Logging;
using Microsoft.Practices.Unity;
using Owin;
using System.Web.Http;
using Felix.Core.Interfaces;
using MassTransit;
using NLog.Internal;
using NLog.Owin.Logging;
using Unity.WebApi;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using Felix.WebHooks.Extensions;
using Telegram.Bot;

namespace Felix.WebHooks
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var http = new HttpConfiguration();

            app.UseWebApi(http)
                .UseNLog()
                .UseContainer(http);
            http.MapHttpAttributeRoutes();
            http.EnsureInitialized();
        }
    }
}