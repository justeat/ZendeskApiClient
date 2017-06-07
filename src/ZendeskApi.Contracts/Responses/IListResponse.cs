using System.Collections.Generic;

namespace ZendeskApi.Contracts.Responses
{
    public interface IListResponse<T>
    {
        IEnumerable<T> Results { get; set; }

        int TotalCount { get; set; }
    }
}