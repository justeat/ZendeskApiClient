using System.Text;
using JE.Api.ClientBase;
using JustEat.ZendeskApi.Client.Factories;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Queries;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public class SearchResource : ISearchResource
    {
        private const string SearchUri = @"/api/v2/search";

        private readonly IBaseClient _client;

        public SearchResource(IBaseClient client)
        {
            _client = client;
        }

        public IListResponse<T> Get<T>(IQueryFactory queryFactory) where T : IZendeskEntity
        {
            var requestUri = _client.BuildUri(SearchUri, queryFactory.BuildQuery());

            return _client.Get<ListResponse<T>>(requestUri);
        }


    }
}
