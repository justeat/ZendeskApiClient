using System;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources.ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class OrganizationMembershipResource : ZendeskResource<OrganizationMembership>, IOrganizationMembershipResource
    {
        private const string UsersUrl = "/api/v2/users/{0}/organization_memberships";
        private const string OrganisationsUrl = "/api/v2/organizations/{0}/organization_memberships";

        public OrganizationMembershipResource(IRestClient client)
        {
            Client = client;
        }

        public IListResponse<OrganizationMembership> GetAllByOrganization(long organizationId)
        {
            string url = string.Format(OrganisationsUrl, organizationId);
            return GetAll<OrganizationMembershipListResponse>(url);
        }

        public async Task<IListResponse<OrganizationMembership>> GetAllByOrganizationAsync(long organizationId)
        {
            string url = string.Format(OrganisationsUrl, organizationId);
            return await GetAllAsync<OrganizationMembershipListResponse>(url).ConfigureAwait(false); ;
        }

        public IListResponse<OrganizationMembership> GetAllByUser(long userId)
        {
            string url = string.Format(UsersUrl, userId);
            return GetAll<OrganizationMembershipListResponse>(url);
        }

        public async Task<IListResponse<OrganizationMembership>> GetAllByUserAsync(long userId)
        {
            string url = string.Format(UsersUrl, userId);
            return await GetAllAsync<OrganizationMembershipListResponse>(url).ConfigureAwait(false);
        }

        public IResponse<OrganizationMembership> Post(OrganizationMembershipRequest request)
        {
            string url = string.Format(UsersUrl, request.Item.UserId);
            return Post<OrganizationMembershipRequest, OrganizationMembershipResponse>(request, url);
        }

        public async Task<IResponse<OrganizationMembership>> PostAsync(OrganizationMembershipRequest request)
        {
            string url = string.Format(UsersUrl, request.Item.UserId);
            return await PostAsync<OrganizationMembershipRequest, OrganizationMembershipResponse>(request, url).ConfigureAwait(false);
        }

        [Obsolete("GetAll is deprecated, please use GetAllByUser or GetAllByOrganization instead.")]
        public IListResponse<OrganizationMembership> GetAll(long id)
        {
            string url = string.Format(UsersUrl, id);
            return GetAll<OrganizationMembershipListResponse>(url);
        }
    }
}
