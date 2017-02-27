using Microsoft.Owin.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Felix.Core.Interfaces;
using Felix.Core.Models;
using GreenPipes.Introspection;
using MassTransit;
using Telegram.Bot;
using Telegram.Bot.Types;
using Message = Felix.Core.Models.Message;

namespace Felix.WebHooks.Controllers
{
    [RoutePrefix("api/webhooks")]
    public class WebHooksController : ApiController
    {
        private IBusControl _bus;
        private readonly ILogger _logger;
        private readonly ITelegramBotClient _client;
        private IBot _bot;

        public WebHooksController(ILogger logger, IBusControl bus, ITelegramBotClient client, IBot bot)
        {
            if (client == null) throw new ArgumentException(nameof(client));
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (bot == null) throw new ArgumentNullException(nameof(bot));

            _client = client;
            _bus = bus;
            _logger = logger;
            _bot = bot;
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
            if (update == null) return BadRequest();
            if (update.Message == null) return BadRequest("Empty message");

            try
            {
                var message = new Message(update.Message?.Text, update.Message?.Date);
                var response = await _bot.GetResponse(message);

                await _client.SendTextMessageAsync(update.Message.Chat.Id, response);

                return Ok(message);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}