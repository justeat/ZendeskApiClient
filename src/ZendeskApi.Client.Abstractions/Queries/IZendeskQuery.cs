using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Queries
{
    public interface IZendeskQuery<T>
    {
        string BuildQuery();

        IZendeskQuery<T> WithCustomFilter(string field, string value, FilterOperator filterOperator);

        IZendeskQuery<T> WithPaging(int pageNumber, int pageSize);

        IZendeskQuery<T> WithOrdering(SortBy sortBy, SortOrder sortOrder);
    }
}