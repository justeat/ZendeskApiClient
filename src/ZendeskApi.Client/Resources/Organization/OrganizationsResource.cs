using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class OrganizationsResource : IOrganizationsResource
    {
        private const string ResourceUri = "api/v2/organizations";

        private const string UserResourceUriFormat = "api/v2/users/{0}/organizations";

        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        private readonly Func<ILogger, string, IDisposable> _loggerScope =
            LoggerMessage.DefineScope<string>(typeof(OrganizationsResource).Name + ": {0}");

        public OrganizationsResource(IZendeskApiClient apiClient,
            ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IPagination<Organization>> GetAllAsync(PagerParameters pager = null)
        {
            using (_loggerScope(_logger, "GetAllAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(ResourceUri, pager).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithHelpDocsLink("organizations#list-organizations")
                        .Build();
                }

                return await response.Content.ReadAsAsync<OrganizationsResponse>();
            }
        }

        public async Task<IPagination<Organization>> GetAllByUserIdAsync(long userId, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"GetAllAsync({userId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(UserResourceUriFormat, userId), pager).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Organization {0} not found", userId);
                    return null;
                }

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithHelpDocsLink("organizations#list-organizations")
                        .Build();
                }

                return await response.Content.ReadAsAsync<OrganizationsResponse>();
            }
        }

        public async Task<Organization> GetAsync(long organizationId)
        {
            using (_loggerScope(_logger, $"GetAsync({organizationId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync(organizationId.ToString()).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Organization {0} not found", organizationId);
                    return null;
                }

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithHelpDocsLink("organizations#show-organization")
                        .Build();
                }

                var result = await response.Content.ReadAsAsync<OrganizationResponse>();
                return result.Organization;
            }
        }

        public async Task<IPagination<Organization>> GetAllAsync(long[] organizationIds, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"GetAllAsync({ZendeskFormatter.ToCsv(organizationIds)})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync($"show_many?ids={ZendeskFormatter.ToCsv(organizationIds)}", pager).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithHelpDocsLink("organizations#show-many-organizations")
                        .Build();
                }

                return await response.Content.ReadAsAsync<OrganizationsResponse>();
            }
        }

        public async Task<IPagination<Organization>> GetAllByExternalIdsAsync(string[] externalIds, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"GetAllByExternalIdsAsync({ZendeskFormatter.ToCsv(externalIds)})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync($"show_many?external_ids={ZendeskFormatter.ToCsv(externalIds)}", pager).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithHelpDocsLink("organizations#show-many-organizations")
                        .Build();
                }

                return await response.Content.ReadAsAsync<OrganizationsResponse>();
            }
        }

        public async Task<Organization> CreateAsync(Organization organization)
        {
            using (_loggerScope(_logger, $"PostAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var request = new OrganizationCreateRequest(organization);
                var response = await client.PostAsJsonAsync(ResourceUri, request).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithHelpDocsLink("organizations#create-organization")
                        .Build();
                }

                var result = await response.Content.ReadAsAsync<OrganizationResponse>();
                return result.Organization;
            }
        }

        public async Task<Organization> UpdateAsync(Organization organization)
        {
            using (_loggerScope(_logger, $"PutAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var request = new OrganizationUpdateRequest(organization);
                var response = await client.PutAsJsonAsync(organization.Id.ToString(), request).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Cannot update organization as organization {0} cannot be found", organization.Id);
                    return null;
                }

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithHelpDocsLink("organizations#update-organization")
                        .Build();
                }

                var result = await response.Content.ReadAsAsync<OrganizationResponse>();
                return result.Organization;
            }
        }

        public async Task DeleteAsync(long organizationId)
        {
            using (_loggerScope(_logger, $"DeleteAsync({organizationId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.DeleteAsync(organizationId.ToString()).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithHelpDocsLink("organizations#delete-organization")
                        .Build();
                }
            }
        }
    }
}
