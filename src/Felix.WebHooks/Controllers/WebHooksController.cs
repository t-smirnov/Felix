using Microsoft.Owin.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GreenPipes.Introspection;
using MassTransit;
using Telegram.Bot.Types;

namespace Felix.WebHooks.Controllers
{
    [RoutePrefix("api/webhoooks")]
    public class WebHooksController : ApiController
    {
        private readonly ILogger _logger;

        public WebHooksController(ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            _logger = logger;
        }

        [HttpGet]
        [Route("version")]
        public async Task<IHttpActionResult> Get()
        {
            var assemblyVersion = Assembly.GetExecutingAssembly()?.FullName;
            var is64os = Environment.Is64BitOperatingSystem;
            var is64proc = Environment.Is64BitProcess;
            var clrVersion = Environment.Version;
            var osVersion = Environment.OSVersion;
            var machineName = Environment.MachineName;
            var processorCount = Environment.ProcessorCount;
            var workingSet = Environment.WorkingSet;

            var result = new
            {
                OS = osVersion,
                Name = machineName,
                ProcCount = processorCount,
                RAM = workingSet,
                CLR = clrVersion,
                Assembly = assemblyVersion,
            };

            return await Task.FromResult(Ok(result));
        }

        [HttpPost]
        [Route("update")]
        public async Task<IHttpActionResult> Post([FromBody]Update update)
        {
            _logger.WriteInformation($"ID:{update.Message.MessageId}{Environment.NewLine}Text:{update.Message.Text}");

            try
            {

                return Ok();
            }
            catch (Exception e)
            {
                _logger.WriteError("Error when publishing message", e);
                throw;
            }
        }
    }
}