using System;
using System.Net;
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

        private readonly Func<ILogger, string, IDisposable> _loggerScope = LoggerMessage.DefineScope<string>(typeof(UsersResource).Name + ": {0}");

        public UsersResource(IZendeskApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<UserListResponse> ListAsync(PagerParameters pager = null)
        {
            using (_loggerScope(_logger, "ListAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(ResourceUri, pager).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithHelpDocsLink("core/users#list-users")
                        .Build();
                }

                return await response.Content.ReadAsAsync<UserListResponse>();
            }
        }

        public async Task<UserListResponse> ListInGroupAsync(long groupId, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"ListInGroupAsync({groupId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(GroupUsersResourceUriFormat, groupId), pager).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Users in group {0} not found", groupId);
                    return null;
                }

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithHelpDocsLink("core/users#list-users")
                        .Build();
                }

                return await response.Content.ReadAsAsync<UserListResponse>();
            }
        }

        public async Task<UserListResponse> ListInOrganizationAsync(long organizationId, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"ListInOrganizationAsync({organizationId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(OrganizationsUsersResourceUriFormat, organizationId), pager).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Users in organization {0} not found", organizationId);
                    return null;
                }

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithHelpDocsLink("core/users#list-users")
                        .Build();
                }

                return await response.Content.ReadAsAsync<UserListResponse>();
            }
        }

        public async Task<UserResponse> GetAsync(long userId)
        {
            using (_loggerScope(_logger, $"GetAsync({userId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync(userId.ToString()).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("UserResponse {0} not found", userId);
                    return null;
                }

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithHelpDocsLink("core/users#show-user")
                        .Build();
                }

                return await response.Content.ReadAsAsync<UserResponse>();
            }
        }
        
        public async Task<UserListResponse> ListAsync(long[] userIds, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"ListAsync({ZendeskFormatter.ToCsv(userIds)})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync($"show_many?ids={ZendeskFormatter.ToCsv(userIds)}", pager).ConfigureAwait(false);
                
                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithHelpDocsLink("core/users#show-many-users")
                        .Build();
                }

                return await response.Content.ReadAsAsync<UserListResponse>();
            }
        }

        public async Task<UserListResponse> ListByExternalIdsAsync(string[] externalIds, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"ListByExternalIdsAsync({ZendeskFormatter.ToCsv(externalIds)})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync($"show_many?external_ids={ZendeskFormatter.ToCsv(externalIds)}", pager).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithHelpDocsLink("core/users#show-many-users")
                        .Build();
                }

                return await response.Content.ReadAsAsync<UserListResponse>();
            }
        }

     /*   public async Task<UserResponse> ListRelatedUsersAsync(long userId)
        {
            using (_loggerScope(_logger, $"ListRelatedUsersAsync({userId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync($"{userId}/related").ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Related Users for user {0} not found", userId);
                    return null;
                }

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithHelpDocsLink("core/users#delete-user")
                        .Build();
                }

                return await response.Content.ReadAsAsync<UserResponse>();
            }
        }*/
        
        public async Task<UserResponse> CreateAsync(UserCreateRequest user)
        {
            using (_loggerScope(_logger, "CreateAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(ResourceUri, new UserRequest<UserCreateRequest>(user)).ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.Created)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithExpectedHttpStatus(HttpStatusCode.Created)
                        .WithHelpDocsLink("core/users#create-user")
                        .Build();
                }

                return await response.Content.ReadAsAsync<UserResponse>();
            }
        }
        
        public async Task<UserResponse> UpdateAsync(UserUpdateRequest user)
        {
            using (_loggerScope(_logger, "UpdateAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PutAsJsonAsync(user.Id.ToString(), new UserRequest<UserUpdateRequest>(user)).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Cannot update user as user {0} cannot be found", user.Id);
                    return null;
                }

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithHelpDocsLink("core/users#update-user")
                        .Build();
                }

                return await response.Content.ReadAsAsync<UserResponse>();
            }
        }

        public async Task DeleteAsync(long userId)
        {
            using (_loggerScope(_logger, "DeleteAsync({userId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.DeleteAsync(userId.ToString()).ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithExpectedHttpStatus(HttpStatusCode.NoContent)
                        .WithHelpDocsLink("core/users#delete-user")
                        .Build();
                }
            }
        }
    }
}
