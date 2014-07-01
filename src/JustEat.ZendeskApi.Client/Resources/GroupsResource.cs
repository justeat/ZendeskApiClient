using JE.Api.ClientBase;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public class GroupsResource : ZendeskResource<Group>, IGroupResource
    {
        protected override string ResourceUri
        {
            get { return @"/api/v2/groups"; }
        }

        public GroupsResource(IBaseClient client)
        {
            Client = client;
        }

        public IResponse<Group> Get(long id)
        {
            return Get<GroupResponse>(id);
        }
    }
}
