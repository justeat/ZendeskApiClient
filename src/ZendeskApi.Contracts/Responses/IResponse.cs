using System.Collections.Generic;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public interface IResponse<T> where T : IZendeskEntity
    {
        T Item { get; set; }
    }

    public interface IBatchResponse<T> where T : IEnumerable<IZendeskEntity>
    {
        T Item { get; set; }
    }
}