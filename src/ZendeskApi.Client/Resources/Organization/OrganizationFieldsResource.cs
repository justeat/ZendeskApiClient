using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class OrganizationFieldsResource : AbstractBaseResource<OrganizationFieldsResource>,
        IOrganizationFieldsResource
    {
        private const string ResourceUri = "api/v2/organization_fields";

        public OrganizationFieldsResource(IZendeskApiClient apiClient, ILogger logger) : base(
            apiClient, logger, "organization_fields")
        {
        }

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        public async Task<IPagination<OrganizationField>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<OrganizationFieldsResponse>(
                ResourceUri,
                "list-organization-fields",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<ICursorPagination<OrganizationField>> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<OrganizationFieldsCursorResponse>(
                ResourceUri,
                "list-organization-fields",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<OrganizationField> GetAsync(
            long orgFieldId,
            CancellationToken cancellationToken = default)
        {
            var response = await GetWithNotFoundCheckAsync<OrganizationFieldResponse>(
                $"{ResourceUri}/{orgFieldId}",
                "show-user-field",
                $"GetAsync({orgFieldId})",
                $"OrganizationField Field {orgFieldId} not found",
                cancellationToken: cancellationToken);

            return response?
                .OrganizationField;
        }

        public async Task<OrganizationField> CreateAsync(
            OrganizationField organizationField,
            CancellationToken cancellationToken = default)
        {
            var response = await CreateAsync<OrganizationFieldResponse, OrganizationFieldCreateUpdateRequest>(
                ResourceUri,
                new OrganizationFieldCreateUpdateRequest(organizationField),
                "create-organization-fields",
                cancellationToken: cancellationToken);

            return response?
                .OrganizationField;
        }

        public async Task<OrganizationField> UpdateAsync(
            OrganizationField organizationField,
            CancellationToken cancellationToken = default)
        {
            var response =
                await UpdateWithNotFoundCheckAsync<OrganizationFieldResponse, OrganizationFieldCreateUpdateRequest>(
                    $"{ResourceUri}/{organizationField.Id}",
                    new OrganizationFieldCreateUpdateRequest(organizationField),
                    "update-organization-fields",
                    $"Cannot update organization field as organization field {organizationField.Id} cannot be found",
                    cancellationToken: cancellationToken);

            return response?
                .OrganizationField;
        }
        
        public async Task DeleteAsync(
            long orgFieldId,
            CancellationToken cancellationToken = default)
        {
            await DeleteAsync(
                ResourceUri,
                orgFieldId,
                "delete-organization-field",
                cancellationToken: cancellationToken);
        }

        public async Task ReorderAsync(OrganizationFieldsReorderRequest request,
            CancellationToken cancellationToken = default)
        {
            await ExecuteRequest(async (client, token) =>
                    await client.PutAsJsonAsync($"{ResourceUri}/reorder", request, cancellationToken: cancellationToken)
                        .ConfigureAwait(false),
                "ReorderAsync", cancellationToken)
                .ThrowIfUnsuccessful("organization_fields#reorder-organization-field");
        }
    }
}