using System.Runtime.CompilerServices;
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

        public T Get<T>(string entity, string customField, int fieldValue) 
        {
            var requestUri = _client.BuildUri(SearchUri, string.Format("query=type:{0}&{1}={2}", entity, customField, fieldValue));

            return _client.Get<T>(requestUri);
        }
    }
}
