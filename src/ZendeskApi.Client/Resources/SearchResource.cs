using System.Threading.Tasks;
using ZendeskApi.Client.Options;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Queries;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class SearchResource : ZendeskResource<IZendeskEntity>, ISearchResource
    {
        private const string SearchUri = "/api/v2/search";
        
        public SearchResource(ZendeskOptions options) : base(options) { }

        public async Task<IListResponse<T>> FindAsync<T>(IZendeskQuery<T> zendeskQuery) where T : IZendeskEntity
        {
            using (var client = CreateZendeskClient(SearchUri + "/"))
            {
                var response = await client.GetAsync($"{SearchUri}?{zendeskQuery.BuildQuery()}").ConfigureAwait(false);
                return await response.Content.ReadAsAsync<ListResponse<T>>();
            }
        }
    }
}
