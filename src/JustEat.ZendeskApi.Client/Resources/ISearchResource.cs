using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Queries;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public interface ISearchResource
    {
        IListResponse<T> Get<T>(IZendeskQuery<T> zendeskQuery) where T : IZendeskEntity;
    }
}