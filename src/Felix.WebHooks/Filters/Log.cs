using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.Owin.Logging;
using NLog.Filters;

namespace Felix.WebHooks.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        private ILogger _logger;

        public LogAttribute(ILogger logger)
        {
            _logger = logger;
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {

            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
    }
}