using System.Threading.Tasks;
using ZendeskApi.Client.Options;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class OrganizationMembershipResource : ZendeskResource<OrganizationMembership>, IOrganizationMembershipResource
    {
        private const string UsersUrl = "/api/v2/users/{0}/organization_memberships";
        private const string OrganisationsUrl = "/api/v2/organizations/{0}/organization_memberships";

        public OrganizationMembershipResource(ZendeskOptions options) : base(options) { }

        public async Task<IListResponse<OrganizationMembership>> GetAllByOrganizationAsync(long organizationId)
        {
            using (var client = CreateZendeskClient("/"))
            {
                var response = await client.GetAsync(string.Format(OrganisationsUrl, organizationId)).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<OrganizationMembershipListResponse>();
            }
        }

        public async Task<IListResponse<OrganizationMembership>> GetAllByUserAsync(long userId)
        {
            using (var client = CreateZendeskClient("/"))
            {
                var response = await client.GetAsync(string.Format(UsersUrl, userId)).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<OrganizationMembershipListResponse>();
            }
        }

        public async Task<IResponse<OrganizationMembership>> PostAsync(OrganizationMembershipRequest request)
        {
            using (var client = CreateZendeskClient("/"))
            {
                var response = await client
                    .PostAsJsonAsync(string.Format(UsersUrl, request.Item.UserId), request).ConfigureAwait(false);

                return await response.Content.ReadAsAsync<OrganizationMembershipResponse>();
            }
        }
    }
}
