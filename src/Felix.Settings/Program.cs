using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felix.Settings
{
    class Program
    {
        static void Main(string[] args)
        {
            var apikey = ConfigurationManager.AppSettings["ApiKey"];
            var bot = new Telegram.Bot.Api(apikey);
        }
    }
}
