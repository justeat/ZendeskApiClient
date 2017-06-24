using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi.Client.Converters
{
    public class SingularJsonConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (typeof(T) != objectType)
            {
                return false;
            }

            var attribute = objectType.GetTypeInfo().GetCustomAttribute<JsonObjectAttribute>();
            if (attribute == null || string.IsNullOrEmpty(attribute.Id))
            {
                return false;
            }

            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var attribute = objectType.GetTypeInfo().GetCustomAttribute<JsonObjectAttribute>();

            var value = serializer.Deserialize<JObject>(reader);

            return value[attribute.Id].ToObject<T>();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var attribute = value.GetType().GetTypeInfo().GetCustomAttribute<JsonObjectAttribute>();

            writer.WriteStartObject();

            writer.WritePropertyName(attribute.Id);
            JToken.FromObject(value).WriteTo(writer);

            writer.WriteEndObject();
        }
    }
}
