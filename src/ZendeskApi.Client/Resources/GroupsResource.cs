using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources.ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class GroupsResource : ZendeskResource<Group>, IGroupResource
    {
        private const string ResourceUri = "/api/v2/groups";

        public GroupsResource(IRestClient client)
        {
            Client = client;
        }

        public IResponse<Group> Get(long id)
        {
            return Get<GroupResponse>($"{ResourceUri}/{id}");
        }

        public async Task<IResponse<Group>> GetAsync(long id)
        {
            return await GetAsync<GroupResponse>($"{ResourceUri}/{id}").ConfigureAwait(false);
        }
    }
}
