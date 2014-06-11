using JE.Api.ClientBase;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public class SearchResource
    {
        private const string SearchUri = @"/api/v2/search";

        private readonly IBaseClient _client;

        public SearchResource(IBaseClient client)
        {
            _client = client;
        }

        public IListResponse<T> Get<T>(ZendeskType type, string customField, int fieldValue) where T : IZendeskEntity
        {
            var requestUri = _client.BuildUri(SearchUri, string.Format("query=type:{0}&{1}={2}", type, customField, fieldValue));

            return _client.Get<ListResponse<T>>(requestUri);
        }
    }
}
