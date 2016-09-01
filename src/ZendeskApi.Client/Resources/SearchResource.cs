using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Queries;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class SearchResource : ISearchResource
    {
        private const string SearchUri = @"/api/v2/search";

        private readonly IRestClient _client;

        public SearchResource(IRestClient client)
        {
            _client = client;
        }

        public IListResponse<T> Find<T>(IZendeskQuery<T> zendeskQuery) where T : IZendeskEntity
        {
            return this.FindAsync(zendeskQuery).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task<IListResponse<T>> FindAsync<T>(IZendeskQuery<T> zendeskQuery) where T : IZendeskEntity
        {
            var requestUri = _client.BuildUri(SearchUri, zendeskQuery.BuildQuery());

            return await _client.GetAsync<ListResponse<T>>(requestUri).ConfigureAwait(false);
        }
    }
}
