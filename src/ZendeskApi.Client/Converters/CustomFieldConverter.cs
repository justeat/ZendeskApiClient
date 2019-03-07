using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Converters
{
    public class CustomFieldConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var customField = (CustomField)value;
            var result = new JObject
            {
                {"id", customField.Id}
            };

            if (customField.Value != null)
            {
                result.Add("value", customField.Value);
            }
            else if (customField.Values == null || customField.Values.Count == 0)
            {
                result.Add("value", null);
            }
            else
            {
                result.Add("value", new JArray(customField.Values));
            }

            result.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var customField = new CustomField()
            {
                Id = (long)token["id"]
            };

            var valueToken = token["value"];
            if (valueToken.Type == JTokenType.Array)
            {
                customField.Values = valueToken.ToObject<List<string>>();
            }
            else
            {
                customField.Value = valueToken.ToObject<string>();
            }

            return customField;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(CustomField);
        }
    }
}
