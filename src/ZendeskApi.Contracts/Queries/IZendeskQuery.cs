using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Queries
{
    public interface IZendeskQuery<T>
    {
        string BuildQuery();

        IZendeskQuery<T> WithCustomFilter(string field, string value);

        IZendeskQuery<T> WithPaging(int pageNumber, int pageSize);

        IZendeskQuery<T> WithOrdering(OrderBy orderBy, Order order);
    }
}