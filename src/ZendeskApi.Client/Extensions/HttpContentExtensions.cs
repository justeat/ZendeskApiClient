using System.IO;
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
            using (var data = await content.ReadAsStreamAsync()) 
            {
                return data.ReadAs<T>();
            }
        }

        public static T ReadAs<T>(this Stream stream)
        {
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var ser = new JsonSerializer();
                ser.Converters.Insert(0, new SingularJsonConverter<T>());
                return ser.Deserialize<T>(jsonReader);
            }
        }
    }
}
