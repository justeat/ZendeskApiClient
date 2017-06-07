using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class AssignableGroupResource :IAssignableGroupResource
    {
        private const string AssignableGroupUri = @"api/v2/groups/assignable";
        private readonly IZendeskApiClient _apiClient;

        public AssignableGroupResource(IZendeskApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ListResponse<Group>> GetAllAsync()
        {
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(AssignableGroupUri).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<GroupListResponse>();
            }
        }
    }
}
