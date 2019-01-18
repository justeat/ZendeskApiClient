using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi.Client.Converters
{
    public class CustomFieldValueConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<string>>();
            }
            return new List<string> { token.ToObject<string>() };
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
