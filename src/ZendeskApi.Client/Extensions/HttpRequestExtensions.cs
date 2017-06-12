using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ZendeskApi.Client
{
    public static class HttpRequestExtensions
    {
        private readonly static JsonSerializerSettings JsonSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public static async Task<HttpResponseMessage> PutAsJsonAsync<T>(
            this HttpClient client,
            string requestUri,
            T value,
            JsonSerializerSettings settings = null)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(value, settings ?? JsonSettings),
                Encoding.UTF8,
                "application/json");

            return await client.PutAsync(requestUri, content).ConfigureAwait(false);
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient client,
            string requestUri,
            T value,
            JsonSerializerSettings settings = null)
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(value, settings ?? JsonSettings),
                Encoding.UTF8,
                "application/json");

            return await client.PostAsync(requestUri, content).ConfigureAwait(false);
        }

        public static async Task<HttpResponseMessage> PostAsBinaryAsync(
            this HttpClient client,
            string requestUri,
            Stream inputStream,
            string fileName)
        {
            using (var content = new MultipartFormDataContent()) // hmm
            {
                content.Add(new StreamContent(inputStream), fileName, fileName);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/binary");

                return await client.PostAsync(requestUri, content).ConfigureAwait(false);
            }
        }

    }
}