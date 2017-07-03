using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Models.Responses;
using ZendeskApi.Client.Requests;

namespace ZendeskApi.Client.Resources
{
    /// <summary>
    /// <see cref="https://developer.zendesk.com/rest_api/docs/core/organization_memberships"/>
    /// </summary>
    public class OrganizationMembershipsResource : IOrganizationMembershipsResource
    {
        private const string ResourceUri = "api/v2/organization_memberships";
        private const string OrganisationsUrlFormat = "api/v2/organizations/{0}/organization_memberships";
        private const string UsersUrlFormat = "api/v2/users/{0}/organization_memberships";

        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        private Func<ILogger, string, IDisposable> _loggerScope =
            LoggerMessage.DefineScope<string>(typeof(OrganizationMembershipsResource).Name + ": {0}");

        public OrganizationMembershipsResource(IZendeskApiClient apiClient,
            ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IPagination<OrganizationMembership>> GetAllAsync(PagerParameters pager = null)
        {
            using (_loggerScope(_logger, "GetAllAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(ResourceUri, pager).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<OrganizationMembershipsResponse>();
            }
        }

        public async Task<IPagination<OrganizationMembership>> GetAllForOrganizationAsync(long organizationId, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"GetAllForOrganizationAsync({organizationId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(OrganisationsUrlFormat, organizationId), pager).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<OrganizationMembershipsResponse>();
            }
        }

        public async Task<IPagination<OrganizationMembership>> GetAllForUserAsync(long userId, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"GetAllForUserAsync({userId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(UsersUrlFormat, userId), pager).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<OrganizationMembershipsResponse>();
            }
        }

        public async Task<OrganizationMembership> GetAsync(long id)
        {
            using (_loggerScope(_logger, $"GetAsync({id})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Requested Organization Membership {0} not found", id);
                    return null;
                }

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<OrganizationMembership>();
            }
        }

        public async Task<OrganizationMembership> GetForUserAndOrganizationAsync(long userId, long organizationId)
        {
            using (_loggerScope(_logger, $"GetForUserAndOrganizationAsync({userId},{organizationId})"))
            using (var client = _apiClient.CreateClient(string.Format(UsersUrlFormat, userId)))
            {
                var response = await client.GetAsync(organizationId.ToString()).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Requested Organization Membership ofr user {0} amd organizaion {1} not found", userId, organizationId);
                    return null;
                }

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<OrganizationMembership>();
            }
        }

        public async Task<OrganizationMembership> CreateAsync(OrganizationMembership organizationMembership)
        {
            using (_loggerScope(_logger, $"PostAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(ResourceUri, organizationMembership).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 201 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/tickets#create-ticket");
                }

                return await response.Content.ReadAsAsync<OrganizationMembership>();
            }
        }

        public async Task<OrganizationMembership> PostForUserAsync(OrganizationMembership organizationMembership, string userId)
        {
            using (_loggerScope(_logger, $"PostAsync({userId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(string.Format(UsersUrlFormat, userId), organizationMembership).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 201 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/tickets#create-ticket");
                }

                return await response.Content.ReadAsAsync<OrganizationMembership>();
            }
        }

        public async Task<JobStatusResponse> CreateAsync(IEnumerable<OrganizationMembership> organizationMemberships)
        {
            using (_loggerScope(_logger, $"PostAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PostAsJsonAsync("create_many", new OrganizationMembershipsRequest { Item = organizationMemberships }).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 201 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/tickets#create-ticket");
                }

                return (await response.Content.ReadAsAsync<JobStatusResponse>());
            }
        }

        public async Task DeleteAsync(long organizationMembershipId)
        {
            using (_loggerScope(_logger, $"DeleteAsync({organizationMembershipId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.DeleteAsync(organizationMembershipId.ToString());

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 204 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/organization_memberships#delete-membership");
                }
            }
        }

        public async Task DeleteAsync(long userId, long organizationMembershipId)
        {
            using (_loggerScope(_logger, $"DeleteAsync({userId},{organizationMembershipId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.DeleteAsync(string.Format(UsersUrlFormat, userId, organizationMembershipId)).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 204 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/organization_memberships#delete-membership");
                }
            }
        }
    }
}
