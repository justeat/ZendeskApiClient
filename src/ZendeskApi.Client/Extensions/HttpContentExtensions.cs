using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZendeskApi.Client.Converters;

namespace ZendeskApi.Client
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            var data = await content.ReadAsStringAsync();

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new SingularJsonConverter<T>());

            return JsonConvert.DeserializeObject<T>(data, settings);
        }
    }
}
