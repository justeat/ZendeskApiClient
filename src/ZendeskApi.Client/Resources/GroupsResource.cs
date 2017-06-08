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
    /// <see cref="https://developer.zendesk.com/rest_api/docs/core/groups"/>
    /// </summary>
    public class GroupsResource : IGroupsResource
    {
        private const string GroupsResourceUri = "api/v2/groups";
        private const string GroupsByUserResourceUriFormat = "api/v2/users/{0}/groups";
        private const string AssignableGroupUri = @"api/v2/groups/assignable";

        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        private Func<ILogger, string, IDisposable> _loggerScope = 
            LoggerMessage.DefineScope<string>("GroupsResource: {0}");

        public GroupsResource(IZendeskApiClient apiClient,
            ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            using (_loggerScope(_logger, "GetAllAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(GroupsResourceUri).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<GroupsResponse>()).Item;
            }
        }

        public async Task<IEnumerable<Group>> GetAllAsync(long userId)
        {
            using (_loggerScope(_logger, $"GetAllAsync({userId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(GroupsByUserResourceUriFormat, userId)).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<GroupsResponse>()).Item;
            }
        }

        public async Task<IEnumerable<Group>> GetAllAssignableAsync()
        {
            using (_loggerScope(_logger, "GetAllAssignableAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(AssignableGroupUri).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<GroupsResponse>()).Item;
            }
        }

        public async Task<Group> GetAsync(long groupId)
        {
            using (_loggerScope(_logger, $"GetAsync({groupId})"))
            using (var client = _apiClient.CreateClient(GroupsResourceUri))
            {
                var response = await client.GetAsync(groupId.ToString()).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<GroupResponse>()).Item;
            }
        }

        public async Task<Group> PostAsync(Group group)
        {
            using (_loggerScope(_logger, "PostAsync")) // Maybe incluse the request in the log?
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(GroupsResourceUri, new GroupRequest { Item = group }).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 201 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/groups#create-groups");
                }
                
                return (await response.Content.ReadAsAsync<GroupResponse>()).Item;
            }
        }

        public async Task<Group> PutAsync(Group group)
        {
            using (_loggerScope(_logger, "PutAsync"))
            using (var client = _apiClient.CreateClient(GroupsResourceUri))
            {
                var response = await client.PutAsJsonAsync(group.Id.ToString(), new GroupRequest { Item = group }).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<GroupResponse>()).Item;
            }
        }

        public async Task DeleteAsync(long groupId)
        {
            using (_loggerScope(_logger, $"DeleteAsync({groupId})"))
            using (var client = _apiClient.CreateClient(GroupsResourceUri))
            {
                var response = await client.DeleteAsync(groupId.ToString());

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 204 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/attachments#delete-upload");
                }
            }
        }
    }
}
