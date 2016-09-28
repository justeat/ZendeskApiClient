using System;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class OrganizationMembershipResource : IOrganizationMembershipResource
    {
        private readonly IRestClient _client;

        public OrganizationMembershipResource(IRestClient client)
        {
            _client = client;
        }

        public IListResponse<OrganizationMembership> GetAllByOrganization(long organizationId)
        {
            var requestUri = _client.BuildUri($"/api/v2/organizations/{organizationId}/organization_memberships");
            return _client.Get<OrganizationMembershipListResponse>(requestUri);
        }

        public async Task<IListResponse<OrganizationMembership>> GetAllByOrganizationAsync(long organizationId)
        {
            var requestUri = _client.BuildUri($"/api/v2/organizations/{organizationId}/organization_memberships");
            return await _client.GetAsync<OrganizationMembershipListResponse>(requestUri).ConfigureAwait(false);
        }

        public IListResponse<OrganizationMembership> GetAllByUser(long userId)
        {
            var requestUri = _client.BuildUri($"/api/v2/users/{userId}/organization_memberships");
            return _client.Get<OrganizationMembershipListResponse>(requestUri);
        }

        public async Task<IListResponse<OrganizationMembership>> GetAllByUserAsync(long userId)
        {
            var requestUri = _client.BuildUri($"/api/v2/users/{userId}/organization_memberships");
            return await _client.GetAsync<OrganizationMembershipListResponse>(requestUri).ConfigureAwait(false);
        }

        public IResponse<OrganizationMembership> Post(OrganizationMembershipRequest request)
        {
            var requestUri = _client.BuildUri($"/api/v2/users/{request.Item.UserId}/organization_memberships");
            return _client.Post<OrganizationMembershipResponse>(requestUri, request);
        }

        public async Task<IResponse<OrganizationMembership>> PostAsync(OrganizationMembershipRequest request)
        {
            var requestUri = _client.BuildUri($"/api/v2/users/{request.Item.UserId}/organization_memberships");
            return await _client.PostAsync<OrganizationMembershipResponse>(requestUri, request).ConfigureAwait(false);
        }

        [Obsolete("GetAll is deprecated, please use GetAllByUser or GetAllByOrganization instead.")]
        public IListResponse<OrganizationMembership> GetAll(long id)
        {
            var requestUri = _client.BuildUri($"/api/v2/users/{id}/organization_memberships");
            return _client.Get<OrganizationMembershipListResponse>(requestUri);
        }
    }
}
