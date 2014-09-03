using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Queries;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ISearchResource
    {
        IListResponse<T> Find<T>(IZendeskQuery<T> zendeskQuery) where T : IZendeskEntity;
    }
}