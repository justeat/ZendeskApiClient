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

        public async Task<IPagination<Organization>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<OrganizationsResponse>(
                ResourceUri,
                "list-organizations",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<IPagination<Organization>> GetAllByUserIdAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetWithNotFoundCheckAsync<OrganizationsResponse>(
                string.Format(UserResourceUriFormat, userId),
                "list-organizations",
                $"GetAllAsync({userId})",
                $"Organization for user {userId} not found",
                pager,
                cancellationToken);
        }

        public async Task<Organization> GetAsync(
            long organizationId,
            CancellationToken cancellationToken = default(CancellationToken))
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

        public async Task<IPagination<Organization>> GetAllAsync(
            long[] organizationIds, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<OrganizationsResponse>(
                $"{ResourceUri}/show_many?ids={ZendeskFormatter.ToCsv(organizationIds)}",
                "show-many-organizations",
                $"GetAllAsync({ZendeskFormatter.ToCsv(organizationIds)})",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<IPagination<Organization>> GetAllByExternalIdsAsync(
            string[] externalIds, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<OrganizationsResponse>(
                $"{ResourceUri}/show_many?external_ids={ZendeskFormatter.ToCsv(externalIds)}",
                "show-many-organizations",
                $"GetAllByExternalIdsAsync({ZendeskFormatter.ToCsv(externalIds)})",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<Organization> CreateAsync(
            ICreateOrganizationOperation organization,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await CreateAsync<OrganizationResponse, OrganizationRequest<CreateOrganizationOperation>>(
                ResourceUri,
                new OrganizationRequest<CreateOrganizationOperation>(new CreateOrganizationOperation(organization)),
                "create-organization",
                cancellationToken: cancellationToken
            );

            return response?
                .Organization;
        }

        public async Task<Organization> UpdateAsync(
            IUpdateOrganizationOperation organization,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await UpdateWithNotFoundCheckAsync<OrganizationResponse, OrganizationRequest<UpdateOrganizationOperation>>(
                $"{ResourceUri}/{organization.Id}",
                new OrganizationRequest<UpdateOrganizationOperation>(new UpdateOrganizationOperation(organization)),
                "update-organization",
                $"Cannot update organization as organization {organization.Id} cannot be found",
                cancellationToken: cancellationToken);

            return response?
                .Organization;
        }

        public async Task DeleteAsync(
            long organizationId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await DeleteAsync(
                ResourceUri,
                organizationId,
                "delete-organization",
                cancellationToken: cancellationToken);
        }
    }
}
