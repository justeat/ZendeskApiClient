using System;
using System.Collections.Generic;
using System.Net.Http;
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
    public class OrganizationMembershipsResource : IOrganizationMembershipsResource
    {
        private const string ResourceUri = "api/v2/organization_memberships";
        private const string OrganisationsUrlFormat = "api/v2/organizations/{0}/organization_memberships";
        private const string UsersUrlFormat = "api/v2/users/{0}/organization_memberships";

        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        private Func<ILogger, string, IDisposable> _loggerScope =
            LoggerMessage.DefineScope<string>("OrganizationMembershipsResource: {0}");

        public OrganizationMembershipsResource(IZendeskApiClient apiClient,
            ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IEnumerable<OrganizationMembership>> GetAllAsync()
        {
            using (_loggerScope(_logger, "GetAllAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(ResourceUri).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<OrganizationMembershipsResponse>()).Item;
            }
        }

        public async Task<IEnumerable<OrganizationMembership>> GetAllForOrganizationAsync(long organizationId)
        {
            using (_loggerScope(_logger, $"GetAllForOrganizationAsync({organizationId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(OrganisationsUrlFormat, organizationId)).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<OrganizationMembershipsResponse>()).Item;
            }
        }

        public async Task<IEnumerable<OrganizationMembership>> GetAllForUserAsync(long userId)
        {
            using (_loggerScope(_logger, $"GetAllForUserAsync({userId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(UsersUrlFormat, userId)).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<OrganizationMembershipsResponse>()).Item;
            }
        }

        public async Task<OrganizationMembership> GetAsync(long id)
        {
            using (_loggerScope(_logger, $"GetAsync({id})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<OrganizationMembershipResponse>()).Item;
            }
        }

        public async Task<OrganizationMembership> GetForUserAndOrganizationAsync(long userId, long organizationId)
        {
            using (_loggerScope(_logger, $"GetForUserAndOrganizationAsync({userId},{organizationId})"))
            using (var client = _apiClient.CreateClient(string.Format(UsersUrlFormat, userId)))
            {
                var response = await client.GetAsync(organizationId.ToString()).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<OrganizationMembershipResponse>()).Item;
            }
        }

        public async Task<OrganizationMembership> PostAsync(OrganizationMembership organizationMembership)
        {
            using (_loggerScope(_logger, $"PostAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(ResourceUri, new OrganizationMembershipRequest { Item = organizationMembership }).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 201 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/tickets#create-ticket");
                }

                return (await response.Content.ReadAsAsync<OrganizationMembershipResponse>()).Item;
            }
        }

        public async Task<OrganizationMembership> PostForUserAsync(OrganizationMembership organizationMembership, string userId)
        {
            using (_loggerScope(_logger, $"PostAsync({userId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(string.Format(UsersUrlFormat, userId), new OrganizationMembershipRequest { Item = organizationMembership }).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 201 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/tickets#create-ticket");
                }

                return (await response.Content.ReadAsAsync<OrganizationMembershipResponse>()).Item;
            }
        }

        public async Task<JobStatus> PostAsync(IEnumerable<OrganizationMembership> organizationMemberships)
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

                return (await response.Content.ReadAsAsync<JobStatusResponse>()).Item;
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
                var response = await client.DeleteAsync(string.Format(UsersUrlFormat, userId, organizationMembershipId));

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
