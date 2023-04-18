using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi.Client.Tests.Extensions
{
    public static class HttpRequestExtensions
    {
        public static async Task<T> ReadAsync<T>(this HttpRequest request)
        {
            using var streamReader = new StreamReader(request.Body, Encoding.UTF8);
            {
                var body = await streamReader.ReadToEndAsync();

                return JsonConvert.DeserializeObject<T>(body, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Converters = { new StringEnumConverter() }
                });
            }
        }
    }
}