using System.Collections.Generic;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Responses
{
    public interface IListResponse<T> where T : IZendeskEntity
    {
        IEnumerable<T> Results { get; set; }

        int TotalCount { get; set; }
    }
}