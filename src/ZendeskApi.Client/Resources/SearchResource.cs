using System.Threading.Tasks;
using ZendeskApi.Contracts.Queries;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class SearchResource : ISearchResource
    {
        private const string SearchUri = "api/v2/search";
        private readonly IZendeskApiClient _apiClient;

        public SearchResource(IZendeskApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IListResponse<T>> FindAsync<T>(IZendeskQuery<T> zendeskQuery)
        {
            using (var client = _apiClient.CreateClient(SearchUri))
            {
                var response = await client.GetAsync($"{SearchUri}?{zendeskQuery.BuildQuery()}").ConfigureAwait(false);
                return await response.Content.ReadAsAsync<ListResponse<T>>();
            }
        }
    }
}
