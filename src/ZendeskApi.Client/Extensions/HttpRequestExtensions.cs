using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi.Client.Extensions
{
    public static class HttpRequestExtensions
    {
        private static JsonSerializerSettings DefaultJsonSettings<T>() {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = { new StringEnumConverter() }
            };
            
            return settings;
        }

        public static async Task<HttpResponseMessage> PutAsJsonAsync<T>(
            this HttpClient client,
            string requestUri,
            T value,
            JsonSerializerSettings settings = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(value, settings ?? DefaultJsonSettings<T>()),
                Encoding.UTF8,
                "application/json");

            return await client.PutAsync(
                    requestUri, 
                    content, 
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient client,
            string requestUri,
            T value,
            JsonSerializerSettings settings = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(value, settings ?? DefaultJsonSettings<T>()),
                Encoding.UTF8,
                "application/json");

            return await client.PostAsync(
                    requestUri, 
                    content,
                    cancellationToken)
                .ConfigureAwait(false);
        }

        /**
         * See https://developer.zendesk.com/rest_api/docs/core/attachments#upload-files
         * The api takes binary files without Multipart boundaries
         */
        public static async Task<HttpResponseMessage> PostAsBinaryAsync(
            this HttpClient client,
            string requestUri,
            Stream inputStream,
            string fileName,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var stream = new MemoryStream())
            {
                inputStream.CopyTo(stream);

                using (var content = new ByteArrayContent(stream.ToArray()))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/binary");

                    return await client.PostAsync(
                            requestUri, 
                            content, 
                            cancellationToken)
                        .ConfigureAwait(false);
                }
            }
        }

    }
}