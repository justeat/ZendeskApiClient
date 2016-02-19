using System;
using Newtonsoft.Json;

namespace ZendeskApi.Contracts
{
    public class CustomFieldBoolConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var stringValue = value.ToString();
            bool boolValue;

            if (bool.TryParse(stringValue, out boolValue))
            {
                writer.WriteValue(boolValue);
            }
            else
            {
                writer.WriteValue(value);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value.ToString();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }
    }
}
