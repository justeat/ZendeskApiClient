using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Requests
{
    public interface IRequest<T> where T: IZendeskEntity
    {
        T Item { get; set; }
    }
}