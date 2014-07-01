using JE.Api.ClientBase;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public class AssignableGroupResource : IAssignableGroupResource
    {
        private const string AssignableGroupUri = @"/api/v2/groups/assignable";

        private readonly IBaseClient _client;

        public AssignableGroupResource(IBaseClient client)
        {
            _client = client;
        }

        public ListResponse<Group> GetAll()
        {
            var requestUri = _client.BuildUri(AssignableGroupUri);

            return _client.Get<GroupListResponse>(requestUri);
        }
    }
}
