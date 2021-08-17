using System;
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

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")] 
        public async Task<IPagination<OrganizationMembership>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<OrganizationMembershipsResponse>(
                ResourceUri,
                "list-memberships",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<OrganizationMembershipsCursorResponse> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<OrganizationMembershipsCursorResponse>(
                ResourceUri,
                "list-memberships",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }


        [Obsolete("Use `GetAllByOrganizationIdAsync` with CursorPager parameter instead.")]
        public async Task<IPagination<OrganizationMembership>> GetAllForOrganizationAsync(
            long organizationId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllByOrganizationIdAsync(
                organizationId,
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllByOrganizationIdAsync` with CursorPager parameter instead.")]
        public async Task<IPagination<OrganizationMembership>> GetAllByOrganizationIdAsync(
            long organizationId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<OrganizationMembershipsResponse>(
                string.Format(OrganisationsUrlFormat, organizationId),
                "list-memberships",
                $"GetAllByOrganizationIdAsync({organizationId})",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<OrganizationMembershipsCursorResponse> GetAllByOrganizationIdAsync(
            long organizationId,
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<OrganizationMembershipsCursorResponse>(
                string.Format(OrganisationsUrlFormat, organizationId),
                "list-memberships",
                $"GetAllByOrganizationIdAsync({organizationId})",
                pager,
                cancellationToken: cancellationToken);
        }

        [Obsolete("Use `GetAllByUserIdAsync` with CursorPager parameter instead.")]
        public async Task<IPagination<OrganizationMembership>> GetAllForUserAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllByUserIdAsync(
                userId,
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllByUserIdAsync` with CursorPager parameter instead.")]
        public async Task<IPagination<OrganizationMembership>> GetAllByUserIdAsync(
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<OrganizationMembershipsResponse>(
                string.Format(UsersUrlFormat, userId),
                "list-memberships",
                $"GetAllByUserIdAsync({userId})",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<OrganizationMembershipsCursorResponse> GetAllByUserIdAsync(
            long userId,
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<OrganizationMembershipsCursorResponse>(
                string.Format(UsersUrlFormat, userId),
                "list-memberships",
                $"GetAllByUserIdAsync({userId})",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<OrganizationMembership> GetAsync(
            long id,
            CancellationToken cancellationToken = default)
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

        [Obsolete("Use `GetByUserIdAndOrganizationIdAsync` instead.")]
        public async Task<OrganizationMembership> GetForUserAndOrganizationAsync(
            long userId, 
            long organizationId,
            CancellationToken cancellationToken = default)
        {
            return await GetByUserIdAndOrganizationIdAsync(
                userId,
                organizationId,
                cancellationToken);
        }

        public async Task<OrganizationMembership> GetByUserIdAndOrganizationIdAsync(
            long userId,
            long organizationId,
            CancellationToken cancellationToken = default)
        {
            var response = await GetWithNotFoundCheckAsync<OrganizationMembershipResponse>(
                $"{string.Format(UsersUrlFormat, userId)}/{organizationId}",
                "show-membership",
                $"GetByUserIdAndOrganizationIdAsync({userId},{organizationId})",
                $"Requested Organization Membership for user {userId} and organization {organizationId} not found",
                cancellationToken: cancellationToken);

            return response?
                .OrganizationMembership;
        }

        public async Task<OrganizationMembership> CreateAsync(
            OrganizationMembership organizationMembership,
            CancellationToken cancellationToken = default)
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

        [Obsolete("Use `PostByUserIdAsync` instead.")]
        public async Task<OrganizationMembership> PostForUserAsync(
            OrganizationMembership organizationMembership, 
            long userId,
            CancellationToken cancellationToken = default)
        {
            return await PostByUserIdAsync(
                organizationMembership,
                userId,
                cancellationToken);
        }

        public async Task<OrganizationMembership> PostByUserIdAsync(
            OrganizationMembership organizationMembership,
            long userId,
            CancellationToken cancellationToken = default)
        {
            var response = await CreateAsync<OrganizationMembershipResponse, OrganizationMembershipCreateRequest>(
                string.Format(UsersUrlFormat, userId),
                new OrganizationMembershipCreateRequest(organizationMembership),
                "create-membership",
                scope: $"PostByUserIdAsync({userId})",
                cancellationToken: cancellationToken
            );

            return response?
                .OrganizationMembership;
        }

        public async Task<JobStatusResponse> CreateAsync(
            IEnumerable<OrganizationMembership> organizationMemberships,
            CancellationToken cancellationToken = default)
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
            CancellationToken cancellationToken = default)
        {
            await DeleteAsync(
                ResourceUri,
                organizationMembershipId,
                "delete-membership",
                cancellationToken: cancellationToken);
        }

        [Obsolete("Use `DeleteByUserIdAsync` instead.")]
        public async Task DeleteAsync(
            long userId, 
            long organizationMembershipId,
            CancellationToken cancellationToken = default)
        {
            await DeleteByUserIdAsync(
                userId,
                organizationMembershipId,
                cancellationToken);
        }

        public async Task DeleteByUserIdAsync(
            long userId,
            long organizationMembershipId,
            CancellationToken cancellationToken = default)
        {
            await DeleteAsync(
                string.Format(DeleteUsersUrlFormat, userId, organizationMembershipId),
                "delete-membership",
                scope: $"DeleteAsync({userId},{organizationMembershipId})",
                cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(
            IEnumerable<long> organizationMembershipIds,
            CancellationToken cancellationToken = default)
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
            CancellationToken cancellationToken = default)
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
