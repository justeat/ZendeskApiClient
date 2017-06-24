using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Convertors
{

    public class PaginationJsonConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            var isit = objectType.GetTypeInfo().IsAssignableFrom(typeof(IPagination<T>).GetTypeInfo());

            return objectType.GetTypeInfo().BaseType == typeof(PaginationResponse<T>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var obj = value as PaginationResponse<T>;
            var iobj = value as IPagination<T>;

            obj.Count = obj.Item.Count();

            writer.WriteStartObject();

            var attribute = obj
                .GetType()
                .GetTypeInfo()
                .DeclaredProperties
                .First(x => x.Name == "Item")
                .GetCustomAttribute<JsonPropertyAttribute>();
            writer.WritePropertyName(attribute.PropertyName);

            writer.WriteStartArray();

            foreach (var item in obj.Item)
            {
                serializer.Serialize(writer, item);
            }

            writer.WriteEndArray();

            var iProperties = iobj
                .GetType()
                .GetTypeInfo();

          //  writer.WritePropertyName(iProperties.GetMember("Count").First().GetCustomAttribute<JsonPropertyAttribute>().PropertyName);
            writer.WriteValue(iobj.Count);

         //   writer.WritePropertyName(iProperties.GetMember("NextPage").First().GetCustomAttribute<JsonPropertyAttribute>().PropertyName);
            writer.WriteValue(iobj.NextPage);

          //  writer.WritePropertyName(iProperties.GetMember("PreviousPage").First().GetCustomAttribute<JsonPropertyAttribute>().PropertyName);
            writer.WriteValue(iobj.PreviousPage);

            writer.WriteEndObject();
        }
    }
}
