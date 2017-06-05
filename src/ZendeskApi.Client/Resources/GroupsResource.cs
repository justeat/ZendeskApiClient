using System.Threading.Tasks;
using ZendeskApi.Client.Options;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class GroupsResource : ZendeskResource<Group>, IGroupResource
    {
        private const string ResourceUri = "/api/v2/groups";

        public GroupsResource(ZendeskOptions options) : base(options) { }

        public async Task<IResponse<Group>> GetAsync(long id)
        {
            using (var client = CreateZendeskClient(ResourceUri + "/"))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<GroupResponse>();
            }
        }
    }
}
