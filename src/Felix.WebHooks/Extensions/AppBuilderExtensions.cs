using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;

namespace Felix.WebHooks.Extensions
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseMassTransit(this IAppBuilder app, string rabbitUri)
        {
            return app;
        }
    }
}