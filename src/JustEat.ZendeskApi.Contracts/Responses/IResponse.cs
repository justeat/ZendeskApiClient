using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Responses
{
    public interface IResponse<T> where T : IZendeskEntity
    {
        T Item { get; set; }
    }
}