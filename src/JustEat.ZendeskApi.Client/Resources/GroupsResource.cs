using JE.Api.ClientBase;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public class GroupsResource : IGroupResource
    {
        protected const string GroupsUri = @"/api/v2/groups";

        private readonly IBaseClient _client;

        public GroupsResource(IBaseClient client)
        {
            _client = client;
        }

        public ListResponse<Group> GetAll()
        {
            var requestUri = _client.BuildUri(GetGroupResource());

            return _client.Get<GroupListResponse>(requestUri);
        }

        protected virtual string GetGroupResource()
        {
            return GroupsUri;
        }
    }
}
