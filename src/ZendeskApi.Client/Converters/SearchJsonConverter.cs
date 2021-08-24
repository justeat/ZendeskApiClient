using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Converters
{
    public class SearchJsonConverter : JsonConverter
    {
        private static readonly Type[] DeserializableSearchTypes =
        {
            typeof(Ticket),
            typeof(UserResponse),
            typeof(Group),
            typeof(Organization)
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsConstructedGenericType && objectType.GetGenericTypeDefinition() == typeof(SearchResponse<>).GetGenericTypeDefinition();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var resultTypes = DeserializableSearchTypes.ToDictionary(
                k => k.GetTypeInfo().GetCustomAttribute<SearchResultTypeAttribute>().ResultType,
                v => v
            );

            var token = serializer
                .Deserialize<JObject>(reader);
            
            var results = token.SelectToken("results")
                .Children()
                .Select(result => (ISearchResult) result.ToObject(resultTypes[result.Value<string>("result_type")]))
                .ToList();

            var nextPage = token.Value<string>("next_page");
            var previousPage = token.Value<string>("previous_page");

            return new SearchResponse<ISearchResult>
            {
                Count = token.Value<int>("count"),
                NextPage = nextPage != null ? new Uri(nextPage) : null,
                PreviousPage = previousPage != null ? new Uri(previousPage) : null,
                Results = results
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
    }
}
