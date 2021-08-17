using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class OrganizationsResource : AbstractBaseResource<OrganizationsResource>,
        IOrganizationsResource
    {
        private const string ResourceUri = "api/v2/organizations";
        private const string UserResourceUriFormat = "api/v2/users/{0}/organizations";

        public OrganizationsResource(
            IZendeskApiClient apiClient,
            ILogger logger) 
            : base(apiClient, logger, "organizations")
        { }

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        public async Task<IPagination<Organization>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<OrganizationsResponse>(
                ResourceUri,
                "list-organizations",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<OrganizationsCursorResponse> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<OrganizationsCursorResponse>(
                ResourceUri,
                "list-organizations",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        [Obsolete("Use `GetAllByUserIdAsync` with CursorPager parameter instead.")]
        public async Task<IPagination<Organization>> GetAllByUserIdAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetWithNotFoundCheckAsync<OrganizationsResponse>(
                string.Format(UserResourceUriFormat, userId),
                "list-organizations",
                $"GetAllAsync({userId})",
                $"Organization for user {userId} not found",
                pager,
                cancellationToken);
        }

        public async Task<OrganizationsCursorResponse> GetAllByUserIdAsync(
            long userId,
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            return await GetWithNotFoundCheckAsync<OrganizationsCursorResponse>(
                string.Format(UserResourceUriFormat, userId),
                "list-organizations",
                $"GetAllAsync({userId})",
                $"Organization for user {userId} not found",
                pager,
                cancellationToken);
        }

        public async Task<Organization> GetAsync(
            long organizationId,
            CancellationToken cancellationToken = default)
        {
            var response = await GetWithNotFoundCheckAsync<OrganizationResponse>(
                $"{ResourceUri}/{organizationId}",
                "show-organization",
                $"GetAsync({organizationId})",
                $"Organization {organizationId} not found",
                cancellationToken: cancellationToken);

            return response?
                .Organization;
        }
        
        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        // Potential naming inconsistency here, should be: GetAllByOrganizationIdsAsync
        public async Task<IPagination<Organization>> GetAllAsync(
            long[] organizationIds, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<OrganizationsResponse>(
                $"{ResourceUri}/show_many?ids={ZendeskFormatter.ToCsv(organizationIds)}",
                "show-many-organizations",
                $"GetAllAsync({ZendeskFormatter.ToCsv(organizationIds)})",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<OrganizationsCursorResponse> GetAllAsync(
            long[] organizationIds,
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<OrganizationsCursorResponse>(
                $"{ResourceUri}/show_many?ids={ZendeskFormatter.ToCsv(organizationIds)}",
                "show-many-organizations",
                $"GetAllAsync({ZendeskFormatter.ToCsv(organizationIds)})",
                pager,
                cancellationToken: cancellationToken);
        }
        
        [Obsolete("Use `GetAllByExternalIdsAsync` with CursorPager parameter instead.")]
        public async Task<IPagination<Organization>> GetAllByExternalIdsAsync(
            string[] externalIds, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<OrganizationsResponse>(
                $"{ResourceUri}/show_many?external_ids={ZendeskFormatter.ToCsv(externalIds)}",
                "show-many-organizations",
                $"GetAllByExternalIdsAsync({ZendeskFormatter.ToCsv(externalIds)})",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<OrganizationsCursorResponse> GetAllByExternalIdsAsync(
            string[] externalIds,
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<OrganizationsCursorResponse>(
                $"{ResourceUri}/show_many?external_ids={ZendeskFormatter.ToCsv(externalIds)}",
                "show-many-organizations",
                $"GetAllByExternalIdsAsync({ZendeskFormatter.ToCsv(externalIds)})",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<Organization> CreateAsync(
            Organization organization,
            CancellationToken cancellationToken = default)
        {
            var response = await CreateAsync<OrganizationResponse, OrganizationCreateRequest>(
                ResourceUri,
                new OrganizationCreateRequest(organization),
                "create-organization",
                cancellationToken: cancellationToken
            );

            return response?
                .Organization;
        }

        public async Task<Organization> UpdateAsync(
            Organization organization,
            CancellationToken cancellationToken = default)
        {
            var response = await UpdateWithNotFoundCheckAsync<OrganizationResponse, OrganizationUpdateRequest>(
                $"{ResourceUri}/{organization.Id}",
                new OrganizationUpdateRequest(organization),
                "update-organization",
                $"Cannot update organization as organization {organization.Id} cannot be found",
                cancellationToken: cancellationToken);

            return response?
                .Organization;
        }

        public async Task<JobStatusResponse> UpdateAsync(IEnumerable<Organization> organizations, CancellationToken cancellationToken = default)
        {
            var response =
                await UpdateAsync<SingleJobStatusResponse, OrganizationListRequest<Organization>>(
                    $"{ResourceUri}/update_many",
                    new OrganizationListRequest<Organization>(organizations),
                    "update-many-organizations",
                    "UpdateAsync",
                    cancellationToken);

            return response?.JobStatus;
        }

        public async Task DeleteAsync(
            long organizationId,
            CancellationToken cancellationToken = default)
        {
            await DeleteAsync(
                ResourceUri,
                organizationId,
                "delete-organization",
                cancellationToken: cancellationToken);
        }
    }
}
