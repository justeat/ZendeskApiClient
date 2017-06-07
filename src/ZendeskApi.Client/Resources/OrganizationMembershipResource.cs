using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class OrganizationMembershipResource : IOrganizationMembershipResource
    {
        private const string UsersUrl = "api/v2/users/{0}/organization_memberships";
        private const string OrganisationsUrl = "api/v2/organizations/{0}/organization_memberships";
        private readonly IZendeskApiClient _apiClient;

        public OrganizationMembershipResource(IZendeskApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IListResponse<OrganizationMembership>> GetAllByOrganizationAsync(long organizationId)
        {
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(OrganisationsUrl, organizationId)).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<OrganizationMembershipListResponse>();
            }
        }

        public async Task<IListResponse<OrganizationMembership>> GetAllByUserAsync(long userId)
        {
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(UsersUrl, userId)).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<OrganizationMembershipListResponse>();
            }
        }

        public async Task<OrganizationMembership> PostAsync(OrganizationMembershipRequest request)
        {
            using (var client = _apiClient.CreateClient())
            {
                var response = await client
                    .PostAsJsonAsync(string.Format(UsersUrl, request.Item.UserId), request).ConfigureAwait(false);
                return (await response.Content.ReadAsAsync<OrganizationMembershipResponse>()).Item;
            }
        }
    }
}
