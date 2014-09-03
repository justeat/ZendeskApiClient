using System.Collections.Generic;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public interface IListResponse<T> where T : IZendeskEntity
    {
        IEnumerable<T> Results { get; set; }

        int TotalCount { get; set; }
    }
}