using JustEat.ZendeskApi.Client.Factories;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public interface ISearchResource
    {
        IListResponse<T> Get<T>(IQueryFactory queryFactory) where T : IZendeskEntity;
    }
}