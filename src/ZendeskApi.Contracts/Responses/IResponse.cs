using ZendeskApi.Contracts.Models;

namespace ZendeskApi.Contracts.Responses
{
    public interface IResponse<T> where T : IZendeskEntity
    {
        T Item { get; set; }
    }
}