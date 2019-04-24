using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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
    public class UsersResource : AbstractBaseResource<UsersResource>, 
        IUsersResource
    {
        private const string ResourceUri = "api/v2/users";
        private const string IncrementalResourceUri = "api/v2/incremental";
        private const string GroupUsersResourceUriFormat = "api/v2/groups/{0}/users";
        private const string OrganizationsUsersResourceUriFormat = "api/v2/organizations/{0}/users";

        public UsersResource(IZendeskApiClient apiClient, ILogger logger) 
            : base(apiClient, logger, "users")
        { }

        public async Task<UsersListResponse> ListAsync(PagerParameters pager = null)
        {
            return await GetAsync<UsersListResponse>(
                ResourceUri,
                "list-users",
                "ListAsync",
                pager);
        }

        public async Task<UsersListResponse> ListInGroupAsync(long groupId, PagerParameters pager = null)
        {
            return await GetWithNotFoundCheckAsync<UsersListResponse>(
                string.Format(GroupUsersResourceUriFormat, groupId),
                "list-users",
                $"ListInGroupAsync({groupId})",
                $"Users in group {groupId} not found",
                pager);
        }

        public async Task<UsersListResponse> ListInOrganizationAsync(long organizationId, PagerParameters pager = null)
        {
            return await GetWithNotFoundCheckAsync<UsersListResponse>(
                string.Format(OrganizationsUsersResourceUriFormat, organizationId),
                "list-users",
                $"ListInOrganizationAsync({organizationId})",
                $"Users in organization {organizationId} not found",
                pager);
        }

        public async Task<UserResponse> GetAsync(long userId)
        {
            var response = await GetWithNotFoundCheckAsync<SingleUserResponse>(
                $"{ResourceUri}/{userId}",
                "show-users",
                $"GetAsync({userId})",
                $"UserResponse {userId} not found");

            return response?
                .UserResponse;
        }
        
        public async Task<UsersListResponse> ListAsync(long[] userIds, PagerParameters pager = null)
        {
            return await GetAsync<UsersListResponse>(
                $"{ResourceUri}/show_many?ids={ZendeskFormatter.ToCsv(userIds)}",
                "show-many-users",
                $"ListAsync({ZendeskFormatter.ToCsv(userIds)})",
                pager);
        }

        public async Task<UsersListResponse> ListByExternalIdsAsync(string[] externalIds, PagerParameters pager = null)
        {
            return await GetAsync<UsersListResponse>(
                $"{ResourceUri}/show_many?external_ids={ZendeskFormatter.ToCsv(externalIds)}",
                "show-many-users",
                $"ListByExternalIdsAsync({ZendeskFormatter.ToCsv(externalIds)})",
                pager);
        }

        public async Task<IncrementalUsersResponse<UserResponse>> GetIncrementalExport(DateTime startTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var nextPage = Convert.ToInt64((startTime - epoch).TotalSeconds);

            return await GetAsync<IncrementalUsersResponse<UserResponse>>(
                $"{IncrementalResourceUri}/users?start_time={nextPage}",
                "incremental-user-export",
                $"GetIncrementalExport");
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
            var response = await CreateAsync<SingleUserResponse, UserRequest<UserCreateRequest>>(
                ResourceUri,
                new UserRequest<UserCreateRequest>(user),
                "create-user");

            return response?
                .UserResponse;
        }
        
        public async Task<UserResponse> UpdateAsync(UserUpdateRequest user)
        {
            var response = await UpdateWithNotFoundCheckAsync<SingleUserResponse, UserRequest<UserUpdateRequest>>(
                $"{ResourceUri}/{user.Id}",
                new UserRequest<UserUpdateRequest>(user),
                "update-ticket",
                $"Cannot update user as user {user.Id} cannot be found");

            return response?
                .UserResponse;
        }

        public async Task<UserResponse> CreateOrUpdateAsync(UserCreateRequest user)
        {
            using (LoggerScope(Logger, "CreateOrUpdateAsync"))
            using (var client = ApiClient.CreateClient(ResourceUri))
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
            await DeleteAsync(
                ResourceUri,
                userId,
                "delete-user",
                HttpStatusCode.OK);
        }
    }
}
