using System;
using System.Linq;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Converters
{
    public class CustomFieldsConverter : JsonConverter
    {
        private static readonly Type[] DeserializableSearchTypes =
        {
            typeof(IReadOnlyCustomFields),
            typeof(ICustomFields)
        };

        public override bool CanConvert(Type objectType)
        {
            return DeserializableSearchTypes.Any(t => t == objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<CustomFields>(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}