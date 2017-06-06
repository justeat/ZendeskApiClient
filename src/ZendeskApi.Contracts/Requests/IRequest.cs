using System.Collections.Generic;
using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    public interface IRequest<T> where T: IZendeskEntity
    {
        T Item { get; set; }
    }

    public interface IBatchRequest<T> where T : IEnumerable<IZendeskEntity>
    {
        T Item { get; set; }
    }
}