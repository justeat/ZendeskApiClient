using System.Collections.Generic;

namespace ZendeskApi.Client.Responses
{
    public interface IListResponse<T>
    {
        IEnumerable<T> Results { get; set; }

        int TotalCount { get; set; }
    }
}