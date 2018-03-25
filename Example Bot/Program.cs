using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            const string basePath = "http://localhost:50192";
            var botApi = new BotApi(basePath);
            var tokenResponse = botApi.BotAquireTokenPost(1, "THIS IS MY SECRET KEY");

            botApi = new BotApi(new Configuration
            {
                BasePath = basePath,
                AccessToken = tokenResponse.Token
            });

            botApi.BotRegisterUserFeedbackPost(new RegisterUserFeedbackRequest(5, 1, "False Positive"));
        }
    }
}
