using System;
using System.Reflection;
using System.Runtime.Serialization;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Models.Tickets;

namespace ZendeskApi.Client.Queries
{
    public static class ZendeskQueryTicketExtensions
    {
        public static IZendeskQuery WithTicketId(this IZendeskQuery query, long id)
        {
            return query.WithFilter(0, null, id.ToString(), FilterOperator.None);
        }

        public static IZendeskQuery WithWord(this IZendeskQuery query, string value, FilterOperator op = FilterOperator.None)
        {
            return query.WithFilter(null, value, op);
        }

        public static IZendeskQuery WithTicketStatus(this IZendeskQuery query, TicketStatus status, FilterOperator op = FilterOperator.Equals)
        {
            return query.WithFilter("status", status.GetType().GetTypeInfo().GetDeclaredField(status.ToString()).GetCustomAttribute<EnumMemberAttribute>().Value, op);
        }

        public static IZendeskQuery WithCreatedDate(this IZendeskQuery query, DateTime dateTime, FilterOperator op)
        {
            return query.WithFilter("created", $"{dateTime:yyyy-MM-dd}", op);
        }

        public static IZendeskQuery FromRequester(this IZendeskQuery query, string email)
        {
            return query.WithFilter("requester", email);
        }

        public static IZendeskQuery WithTags(this IZendeskQuery query, params string[] tags)
        {
            return query.WithFilter("tags", string.Join(",", tags));
        }

        public static IZendeskQuery WithCustomFieldValue(this IZendeskQuery query, string customFieldValue)
        {
            return query.WithFilter("fieldvalue", customFieldValue);
        }
    }
}