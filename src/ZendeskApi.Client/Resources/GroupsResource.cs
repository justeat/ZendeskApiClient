using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class GroupsResource : ZendeskResource<Group>, IGroupResource
    {
        protected override string ResourceUri => @"/api/v2/groups";

        public GroupsResource(IRestClient client)
        {
            Client = client;
        }

        public IResponse<Group> Get(long id)
        {
            return GetAsync(id).Result;
        }

        public async Task<IResponse<Group>> GetAsync(long id)
        {
            return await GetAsync<GroupResponse>(id).ConfigureAwait(false);
        }
    }
}
