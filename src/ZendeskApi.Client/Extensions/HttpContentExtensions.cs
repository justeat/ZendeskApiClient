using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Reflection;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent content) {
            var data = await content.ReadAsStringAsync();

            var isit = typeof(T).GetTypeInfo().IsAssignableFrom(typeof(IPagination<T>).GetTypeInfo());

            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
