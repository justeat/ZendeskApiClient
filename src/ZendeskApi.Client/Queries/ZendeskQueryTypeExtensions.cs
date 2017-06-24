using System.Reflection;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Queries
{
    public static class ZendeskQueryTypeExtensions
    {
        public static IZendeskQuery WithTypeFilter<T>(this IZendeskQuery query)
        {
            var id = typeof(T).GetTypeInfo().GetCustomAttribute<JsonObjectAttribute>().Id;

            return query.WithFilter("type", id, Models.FilterOperator.Equals);
        }
    }
}