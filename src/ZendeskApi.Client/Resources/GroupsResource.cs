using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class GroupsResource : IGroupResource
    {
        private const string ResourceUri = "api/v2/groups";
        private readonly IZendeskApiClient _apiClient;

        public GroupsResource(IZendeskApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<Group> GetAsync(long id)
        {
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                return (await response.Content.ReadAsAsync<GroupResponse>()).Item;
            }
        }
    }
}
