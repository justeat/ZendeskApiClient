using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Queries
{
    public interface IZendeskQuery
    {
        string BuildQuery();

        IZendeskQuery WithFilter(string field, string value, FilterOperator filterOperator = FilterOperator.Equals);
        IZendeskQuery WithFilter(int index, string field, string value, FilterOperator filterOperator = FilterOperator.Equals);

        IZendeskQuery WithOrdering(SortBy sortBy, SortOrder sortOrder);
    }
}