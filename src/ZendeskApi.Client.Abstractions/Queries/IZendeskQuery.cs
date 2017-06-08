using ZZendeskApi.ClientModels;

namespace ZendeskApi.Client.Queries
{
    public interface IZendeskQuery<T>
    {
        string BuildQuery();

        IZendeskQuery<T> WithCustomFilter(string field, string value, FilterOperator filterOperator);

        IZendeskQuery<T> WithPaging(int pageNumber, int pageSize);

        IZendeskQuery<T> WithOrdering(OrderBy orderBy, Order order);
    }
}