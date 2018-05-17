using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Higgs.Server.Test
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string url, object payload)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            return client.PostAsync(url, stringContent);
        }

        public static async Task AssertError(this HttpResponseMessage message, HttpStatusCode code, string errorMessage = null)
        {
            Assert.AreEqual(code, message.StatusCode);
            if (errorMessage != null)
            {
                var payloadStr = await message.Content.ReadAsStringAsync();
                var payload = JsonConvert.DeserializeObject<ErrorWrapper>(payloadStr);
                Assert.AreEqual(errorMessage, payload.Error);
            }
        }
    }
}
