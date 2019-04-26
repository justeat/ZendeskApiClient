using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    /// <summary>
    /// <see cref="https://developer.zendesk.com/rest_api/docs/core/organization_memberships"/>
    /// </summary>
    public class OrganizationMembershipsResource : AbstractBaseResource<OrganizationMembershipsResource>, 
        IOrganizationMembershipsResource
    {
        private const string ResourceUri = "api/v2/organization_memberships";
        private const string OrganisationsUrlFormat = "api/v2/organizations/{0}/organization_memberships";
        private const string UsersUrlFormat = "api/v2/users/{0}/organization_memberships";
        private const string DeleteUsersUrlFormat = "api/v2/users/{0}/organization_memberships/{1}";

        public OrganizationMembershipsResource(
            IZendeskApiClient apiClient,
            ILogger logger)
            : base(apiClient, logger, "organization_memberships")
        { }

        public async Task<IPagination<OrganizationMembership>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<OrganizationMembershipsResponse>(
                ResourceUri,
                "list-memberships",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<IPagination<OrganizationMembership>> GetAllForOrganizationAsync(
            long organizationId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<OrganizationMembershipsResponse>(
                string.Format(OrganisationsUrlFormat, organizationId),
                "list-memberships",
                $"GetAllForOrganizationAsync({organizationId})",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<IPagination<OrganizationMembership>> GetAllForUserAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<OrganizationMembershipsResponse>(
                string.Format(UsersUrlFormat, userId),
                "list-memberships",
                $"GetAllForUserAsync({userId})",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<OrganizationMembership> GetAsync(
            long id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await GetWithNotFoundCheckAsync<OrganizationMembershipResponse>(
                $"{ResourceUri}/{id}",
                "show-membership",
                $"GetAsync({id})",
                $"Requested Organization Membership {id} not found",
                cancellationToken: cancellationToken);

            return response?
                .OrganizationMembership;
        }

        public async Task<OrganizationMembership> GetForUserAndOrganizationAsync(
            long userId, 
            long organizationId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await GetWithNotFoundCheckAsync<OrganizationMembershipResponse>(
                $"{string.Format(UsersUrlFormat, userId)}/{organizationId}",
                "show-membership",
                $"GetForUserAndOrganizationAsync({userId},{organizationId})",
                $"Requested Organization Membership for user {userId} and organization {organizationId} not found",
                cancellationToken: cancellationToken);

            return response?
                .OrganizationMembership;
        }

        public async Task<OrganizationMembership> CreateAsync(
            OrganizationMembership organizationMembership,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await CreateAsync<OrganizationMembershipResponse, OrganizationMembershipCreateRequest>(
                ResourceUri,
                new OrganizationMembershipCreateRequest(organizationMembership),
                "create-membership",
                cancellationToken: cancellationToken
            );

            return response?
                .OrganizationMembership;
        }

        public async Task<OrganizationMembership> PostForUserAsync(
            OrganizationMembership organizationMembership, 
            long userId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await CreateAsync<OrganizationMembershipResponse, OrganizationMembershipCreateRequest>(
                string.Format(UsersUrlFormat, userId),
                new OrganizationMembershipCreateRequest(organizationMembership),
                "create-membership",
                scope: $"PostAsync({userId})",
                cancellationToken: cancellationToken
            );

            return response?
                .OrganizationMembership;
        }

        public async Task<JobStatusResponse> CreateAsync(
            IEnumerable<OrganizationMembership> organizationMemberships,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await CreateAsync<JobStatusResponse, OrganizationMembershipsRequest>(
                $"{ResourceUri}/create_many",
                new OrganizationMembershipsRequest { Item = organizationMemberships },
                "create-many-memberships",
                HttpStatusCode.OK,
                "PostAsync",
                cancellationToken
            );
        }

        public async Task DeleteAsync(
            long organizationMembershipId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await DeleteAsync(
                ResourceUri,
                organizationMembershipId,
                "delete-membership",
                cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(
            long userId, 
            long organizationMembershipId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await DeleteAsync(
                string.Format(DeleteUsersUrlFormat, userId, organizationMembershipId),
                "delete-membership",
                scope: $"DeleteAsync({userId},{organizationMembershipId})",
                cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(
            IEnumerable<long> organizationMembershipIds,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await DeleteAsync(
                $"{ResourceUri}/destroy_many.json",
                organizationMembershipIds.ToArray(),
                "bulk-delete-memberships",
                cancellationToken: cancellationToken);
        }

        public async Task<IPagination<OrganizationMembership>> MakeDefault(
            long userId, 
            long organizationMembershipId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await UpdateAsync<OrganizationMembershipsResponse, object>(
                $"{string.Format(UsersUrlFormat, userId)}/{organizationMembershipId}/make_default.json",
                new { },
                "set-membership-as-default",
                "MakeDefault",
                cancellationToken);
        }
    }
}
