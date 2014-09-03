using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Requests
{
    public interface IRequest<T> where T: IZendeskEntity
    {
        T Item { get; set; }
    }
}