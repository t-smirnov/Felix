using Microsoft.Owin.Cors;
using Microsoft.Owin.Logging;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Telegram.Bot.Types;

namespace Felix.WebHooks
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            
            app.UseWebApi(config);
            app.UseCors(CorsOptions.AllowAll);

            var container = new UnityContainer();

            var logger = new Microsoft.Owin.Logging.DiagnosticsLoggerFactory().Create("Default");

            container.RegisterInstance<ILogger>(logger);
            
            var update = new Update();
            var json = JsonConvert.SerializeObject(update);

        }
    }
}