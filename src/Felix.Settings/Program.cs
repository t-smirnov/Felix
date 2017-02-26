using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Felix.Settings
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("# Welcome to bot settings");
            Console.Write("# Please provider your token (or press enter): ");
            var token = Console.ReadLine();
            if (string.IsNullOrEmpty(token))
            {
                token = ConfigurationManager.AppSettings["token"];
                Console.WriteLine($"# Token from config file: {token}");
            }
            var client = new Telegram.Bot.TelegramBotClient(token);
            var task = client.GetMeAsync();
            task.Wait();
            if (task.IsCompleted)
            {
                var user = task.Result;
                Console.WriteLine($"{user.FirstName} {user.LastName} is greeting you here!");
            }

            try
            {
                var stream = new FileStream("C:\\Users\\tsmirnov\\ssl\\fpublic.pem", FileMode.Open);
                
                var cert = new FileToSend()
                {
                    Content = stream,
                    Filename = "felix"           
                };
                client.SetWebhookAsync("https://cloundwin.westeurope.cloudapp.azure.com/api/webhooks/update", cert).Wait();
                client.StartReceiving();
                
                var message = new Message()
                {

                };


            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
