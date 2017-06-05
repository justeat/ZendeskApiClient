using System.Threading.Tasks;
using ZendeskApi.Client.Options;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class AssignableGroupResource : ZendeskResource<Group>, IAssignableGroupResource
    {
        private const string AssignableGroupUri = @"/api/v2/groups/assignable";
        
        public AssignableGroupResource(ZendeskOptions options) : base(options) { }
        
        public async Task<ListResponse<Group>> GetAllAsync()
        {
            using (var client = CreateZendeskClient("/"))
            {
                var response = await client.GetAsync(AssignableGroupUri).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<GroupListResponse>();
            }
        }
    }
}
