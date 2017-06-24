using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Queries
{
    public interface IZendeskQuery
    {
        string BuildQuery();

        IZendeskQuery WithFilter(string field, string value, FilterOperator filterOperator);

        IZendeskQuery WithOrdering(SortBy sortBy, SortOrder sortOrder);
    }
}