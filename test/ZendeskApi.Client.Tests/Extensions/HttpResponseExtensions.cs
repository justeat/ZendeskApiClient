using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Tests.Extensions
{
    public static class HttpResponseExtensions
    {
        public static Task WriteAsJson<T>(this HttpResponse response, T value) {

            var settings = new JsonSerializerSettings();

            return response.WriteAsync(JsonConvert.SerializeObject(value, settings));
        }
    }
}