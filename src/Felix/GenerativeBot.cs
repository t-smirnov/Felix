using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlchemyAPIClient;
using AlchemyAPIClient.Requests;
using Felix.Core.Interfaces;
using Felix.Core.Models;

namespace Felix
{
    public class GenerativeBot : IBot
    {
        private readonly AlchemyClient _client;

        public GenerativeBot(AlchemyClient client)
        {
            _client = client;


        }

        public async Task<string> GetResponse(Message message)
        {
            var request = new AlchemyTextEntitiesRequest(message.Text, _client);
            try
            {
                var response = await request.GetResponse();
                return response.Text;

            }
            catch (Exception e)
            {
                return "Ок, я понял";
            }

        }
    }
}
