using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Converters
{
    public class JobStatusResultConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    JObject item = JObject.Load(reader);
                    var itemAsResult = item.ToObject<JobStatusResult>();
                    return new [] {itemAsResult};
                case JsonToken.StartArray:
                {
                    JArray array = JArray.Load(reader);
                    return array.ToObject<IEnumerable<JobStatusResult>>();
                }
            }

            return null; //We shouldn't ever be getting something other than an Object/Array.
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}