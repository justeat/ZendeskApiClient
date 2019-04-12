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
        private const string IncrementalResourceUri = "api/v2/incremental";
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

        public async Task<UsersListResponse> ListAsync(PagerParameters pager = null)
        {
            using (_loggerScope(_logger, "ListAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(ResourceUri, pager).ConfigureAwait(false);

                await response.ThrowIfUnsuccessful("users#list-users");

                return await response.Content.ReadAsAsync<UsersListResponse>();
            }
        }

        public async Task<UsersListResponse> ListInGroupAsync(long groupId, PagerParameters pager = null)
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

                await response.ThrowIfUnsuccessful("users#list-users");

                return await response.Content.ReadAsAsync<UsersListResponse>();
            }
        }

        public async Task<UsersListResponse> ListInOrganizationAsync(long organizationId, PagerParameters pager = null)
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

                await response.ThrowIfUnsuccessful("users#list-users");

                return await response.Content.ReadAsAsync<UsersListResponse>();
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

                await response.ThrowIfUnsuccessful("users#show-users");

                var result = await response.Content.ReadAsAsync<SingleUserResponse>();
                return result.UserResponse;
            }
        }
        
        public async Task<UsersListResponse> ListAsync(long[] userIds, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"ListAsync({ZendeskFormatter.ToCsv(userIds)})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync($"show_many?ids={ZendeskFormatter.ToCsv(userIds)}", pager).ConfigureAwait(false);

                await response.ThrowIfUnsuccessful("users#show-many-users");

                return await response.Content.ReadAsAsync<UsersListResponse>();
            }
        }

        public async Task<UsersListResponse> ListByExternalIdsAsync(string[] externalIds, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"ListByExternalIdsAsync({ZendeskFormatter.ToCsv(externalIds)})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync($"show_many?external_ids={ZendeskFormatter.ToCsv(externalIds)}", pager).ConfigureAwait(false);

                await response.ThrowIfUnsuccessful("users#show-many-users");

                return await response.Content.ReadAsAsync<UsersListResponse>();
            }
        }

        public async Task<IncrementalUsersResponse<UserResponse>> GetIncrementalExport(DateTime startTime)
        {
            using (_loggerScope(_logger, "GetIncrementalExport"))
            using (var client = _apiClient.CreateClient(IncrementalResourceUri))
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var nextPage = Convert.ToInt64((startTime - epoch).TotalSeconds);

                var response = await client.GetAsync($"users?start_time={nextPage}").ConfigureAwait(false);

                await response.ThrowIfUnsuccessful("incremental_export#incremental-user-export");

                return await response.Content.ReadAsAsync<IncrementalUsersResponse<UserResponse>>();
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
                    await response.ThrowZendeskRequestException(
                        "users#create-user",
                        HttpStatusCode.Created);
                }

                var result = await response.Content.ReadAsAsync<SingleUserResponse>();
                return result.UserResponse;
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

                await response.ThrowIfUnsuccessful("users#update-user");

                var result = await response.Content.ReadAsAsync<SingleUserResponse>();
                return result.UserResponse;
            }
        }

        public async Task<UserResponse> CreateOrUpdateAsync(UserCreateRequest user)
        {
            using (_loggerScope(_logger, "CreateOrUpdateAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PostAsJsonAsync("create_or_update", new UserRequest<UserCreateRequest>(user)).ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.Created && response.StatusCode != HttpStatusCode.OK)
                {
                    await response.ThrowZendeskRequestException(
                        "users#create-or-update-user",
                        new []{ HttpStatusCode.Created, HttpStatusCode.OK });
                }

                var result = await response.Content.ReadAsAsync<SingleUserResponse>();
                return result.UserResponse;
            }
        }

        public async Task DeleteAsync(long userId)
        {
            using (_loggerScope(_logger, "DeleteAsync({userId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.DeleteAsync(userId.ToString()).ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    await response.ThrowZendeskRequestException(
                        "users#delete-user",
                        HttpStatusCode.OK);
                }
            }
        }
    }
}
