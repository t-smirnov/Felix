﻿using Microsoft.Owin.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Telegram.Bot.Types;

namespace Felix.WebHooks.Controllers
{
    [RoutePrefix("api/telegram")]
    public class TelegramController : ApiController
    {
        private ILogger _logger;

        public TelegramController(ILogger logger)
        {
            _logger = logger;        
        }

        [HttpPost]
        [Route("update")]
        public async Task<IHttpActionResult> Post(Update update)
        {
            var serialized = JsonConvert.SerializeObject(update);
            _logger.WriteInformation(serialized);
            return await Task.FromResult(Ok($"I got {serialized}"));
        }

        
    }
}