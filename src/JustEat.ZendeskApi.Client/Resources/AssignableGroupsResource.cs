using JE.Api.ClientBase;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public class AssignableGroupResource : IAssignableGroupResource
    {
        private const string AssignableGroupUri = @"/api/v2/groups/assignable";

        private readonly IZendeskClient _client;

        public AssignableGroupResource(IZendeskClient client)
        {
            _client = client;
        }

        public ListResponse<Group> GetAll()
        {
            var requestUri = _client.BuildZendeskUri(AssignableGroupUri);

            return _client.Get<GroupListResponse>(requestUri);
        }
    }
}
