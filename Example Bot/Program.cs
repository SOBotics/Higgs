using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example_Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }


        static async Task MainAsync(string[] args)
        {
            const string basePath = "http://localhost:50192";
            var botApi = new BotApi(basePath);
            var tokenResponse = botApi.BotAquireTokenPost(1, "THIS IS MY SECRET KEY");

            botApi = new BotApi(new Configuration
            {
                BasePath = basePath,
                AccessToken = tokenResponse.Token
            });

            var ws = new ClientWebSocket();
            await ws.ConnectAsync(new Uri("wss://chat.sockets.stackexchange.com/events/111347/c879a2406e0a469b8ac0e84155d062ca?l=84751706"), CancellationToken.None);
            // ws.ReceiveAsync()
            // botApi.BotRegisterUserFeedbackPost(new RegisterUserFeedbackRequest(5, 1, "False Positive"));
        }
    }
}
