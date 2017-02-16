using Microsoft.Owin.Cors;
using Microsoft.Owin.Logging;
using Microsoft.Practices.Unity;
using Owin;
using System.Web.Http;
using NLog.Owin.Logging;
using Unity.WebApi;

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
            app.UseNLog();
            var container = new UnityContainer();

            container.RegisterInstance<ILogger>(app.GetLoggerFactory().Create("WebHooks"));

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}