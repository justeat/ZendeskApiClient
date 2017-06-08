using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    /// <summary>
    /// <see cref="https://developer.zendesk.com/rest_api/docs/core/users"/>
    /// </summary>
    public class UsersResource : IUsersResource
    {
        private const string ResourceUri = "api/v2/users";
        private const string GroupUsersResourceUriFormat = "api/v2/groups/{0}/users";
        private const string OrganizationsUsersResourceUriFormat = "api/v2/organizations/{0}/users";

        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        private Func<ILogger, string, IDisposable> _loggerScope =
            LoggerMessage.DefineScope<string>("UsersResource: {0}");

        public UsersResource(IZendeskApiClient apiClient,
            ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (_loggerScope(_logger, $"GetAllAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(ResourceUri).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<UsersResponse>()).Item;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersInGroupAsync(long groupId)
        {
            using (_loggerScope(_logger, $"GetAllUsersInGroupAsync({groupId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(GroupUsersResourceUriFormat, groupId)).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<UsersResponse>()).Item;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersInOrganizationAsync(long organizationId)
        {
            using (_loggerScope(_logger, $"GetAllUsersInOrganizationAsync({organizationId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(OrganizationsUsersResourceUriFormat, organizationId)).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<UsersResponse>()).Item;
            }
        }

        public async Task<User> GetAsync(long userId)
        {
            using (_loggerScope(_logger, $"GetAsync({userId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync(userId.ToString()).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<UserResponse>()).Item;
            }
        }
        
        public async Task<IEnumerable<User>> GetAllAsync(long[] userIds)
        {
            using (_loggerScope(_logger, $"GetAllAsync({ZendeskFormatter.ToCsv(userIds)})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync($"show_many?ids={ZendeskFormatter.ToCsv(userIds)}").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<UsersResponse>()).Item;
            }
        }

        public async Task<IEnumerable<User>> GetAllByExternalIdsAsync(long[] externalIds)
        {
            using (_loggerScope(_logger, $"GetAllByExternalIdsAsync({ZendeskFormatter.ToCsv(externalIds)})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync($"show_many?external_ids={ZendeskFormatter.ToCsv(externalIds)}").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<UsersResponse>()).Item;
            }
        }

        public async Task<User> GetRelatedUsersAsync(long userId)
        {
            using (_loggerScope(_logger, $"GetRelatedUsersAsync({userId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync(userId.ToString() + "/related").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<UserResponse>()).Item;
            }
        }
        
        public async Task<User> PostAsync(User user)
        {
            using (_loggerScope(_logger, $"PostAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(ResourceUri, new UserRequest { Item = user }).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 201 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/users#create-user");
                }

                return (await response.Content.ReadAsAsync<UserResponse>()).Item;
            }
        }
        
        public async Task<User> PutAsync(User user)
        {
            using (_loggerScope(_logger, $"PutAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PutAsJsonAsync(user.Id.ToString(), new UserRequest { Item = user }).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<UserResponse>()).Item;
            }
        }

        public async Task DeleteAsync(long userId)
        {
            using (_loggerScope(_logger, $"DeleteAsync({userId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.DeleteAsync(userId.ToString());

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 204 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/users#delete-user");
                }
            }
        }
    }
}
