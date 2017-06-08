using System.IO;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Tests
{
    public static class StreamExtensions
    {
        public static T Deserialize<T>(this Stream s)
        {
            using (StreamReader reader = new StreamReader(s))
            using (JsonTextReader jsonReader = new JsonTextReader(reader))
            {
                JsonSerializer ser = new JsonSerializer();
                return ser.Deserialize<T>(jsonReader);
            }
        }
    }
}
