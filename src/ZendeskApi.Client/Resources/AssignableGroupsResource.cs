using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class AssignableGroupResource : IAssignableGroupResource
    {
        private const string AssignableGroupUri = @"/api/v2/groups/assignable";

        private readonly IRestClient _client;

        public AssignableGroupResource(IRestClient client)
        {
            _client = client;
        }

        public ListResponse<Group> GetAll()
        {
            return GetAllAsync().Result;
        }

        public async Task<ListResponse<Group>> GetAllAsync()
        {
            var requestUri = _client.BuildUri(AssignableGroupUri);

            return await _client.GetAsync<GroupListResponse>(requestUri).ConfigureAwait(false);
        }
    }
}
