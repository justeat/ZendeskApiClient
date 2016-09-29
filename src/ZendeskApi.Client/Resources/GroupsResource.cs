using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class GroupsResource : IGroupResource
    {
        private readonly IRestClient _client;
        private const string ResourceUri = @"/api/v2/groups";

        public GroupsResource(IRestClient client)
        {
            _client = client;
        }

        public IResponse<Group> Get(long id)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            return _client.Get<GroupResponse>(requestUri);
        }

        public async Task<IResponse<Group>> GetAsync(long id)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            return await _client.GetAsync<GroupResponse>(requestUri).ConfigureAwait(false);
        }
    }
}
