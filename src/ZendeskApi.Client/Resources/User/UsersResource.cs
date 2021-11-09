using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Requests.User;
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

        public UsersResource(
            IZendeskApiClient apiClient, 
            ILogger logger) 
            : base(apiClient, logger, "users")
        { }

        #region List Users
        [Obsolete("Use `GetAllAsync` instead.")]
        public async Task<UsersListResponse> ListAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllAsync(
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllByGroupIdAsync` instead.")]
        public async Task<UsersListResponse> ListInGroupAsync(
            long groupId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllByGroupIdAsync(
                groupId,
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllByOrganizationIdAsync` instead.")]
        public async Task<UsersListResponse> ListInOrganizationAsync(
            long organizationId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllByOrganizationIdAsync(
                organizationId,
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllAsync` instead.")]
        public async Task<UsersListResponse> ListAsync(
            long[] userIds,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllAsync(
                userIds,
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllByExternalIdsAsync` instead.")]
        public async Task<UsersListResponse> ListByExternalIdsAsync(
            string[] externalIds,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllByExternalIdsAsync(
                externalIds,
                pager,
                cancellationToken);
        }
        #endregion

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        public async Task<UsersListResponse> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<UsersListResponse>(
                ResourceUri,
                "list-users",
                "ListAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<UsersListCursorResponse> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<UsersListCursorResponse>(
                ResourceUri,
                "list-users",
                "ListAsync",
                pager,
                cancellationToken: cancellationToken);
        }
        
        [Obsolete("Use `GetAllByGroupIdAsync` with CursorPager parameter instead.")]
        public async Task<UsersListResponse> GetAllByGroupIdAsync(
            long groupId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetWithNotFoundCheckAsync<UsersListResponse>(
                string.Format(GroupUsersResourceUriFormat, groupId),
                "list-users",
                $"ListInGroupAsync({groupId})",
                $"Users in group {groupId} not found",
                pager,
                cancellationToken);
        }

        public async Task<UsersListCursorResponse> GetAllByGroupIdAsync(
            long groupId,
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            return await GetWithNotFoundCheckAsync<UsersListCursorResponse>(
                string.Format(GroupUsersResourceUriFormat, groupId),
                "list-users",
                $"ListInGroupAsync({groupId})",
                $"Users in group {groupId} not found",
                pager,
                cancellationToken);
        }
        
        [Obsolete("Use `GetAllByGroupIdAsync` with CursorPager parameter instead.")]
        public async Task<UsersListResponse> GetAllByOrganizationIdAsync(
            long organizationId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetWithNotFoundCheckAsync<UsersListResponse>(
                string.Format(OrganizationsUsersResourceUriFormat, organizationId),
                "list-users",
                $"ListInOrganizationAsync({organizationId})",
                $"Users in organization {organizationId} not found",
                pager,
                cancellationToken);
        }

        public async Task<UsersListCursorResponse> GetAllByOrganizationIdAsync(
            long organizationId,
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            return await GetWithNotFoundCheckAsync<UsersListCursorResponse>(
                string.Format(OrganizationsUsersResourceUriFormat, organizationId),
                "list-users",
                $"ListInOrganizationAsync({organizationId})",
                $"Users in organization {organizationId} not found",
                pager,
                cancellationToken);
        }

        public async Task<UsersListResponse> GetAllByExternalIdsAsync(
            string[] externalIds,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<UsersListResponse>(
                $"{ResourceUri}/show_many?external_ids={ZendeskFormatter.ToCsv(externalIds)}",
                "show-many-users",
                $"ListByExternalIdsAsync({ZendeskFormatter.ToCsv(externalIds)})",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<UserResponse> GetAsync(
            long userId,
            CancellationToken cancellationToken = default)
        {
            var response = await GetWithNotFoundCheckAsync<SingleUserResponse>(
                $"{ResourceUri}/{userId}",
                "show-users",
                $"GetAsync({userId})",
                $"UserResponse {userId} not found",
                cancellationToken: cancellationToken);

            return response?
                .UserResponse;
        }

        public async Task<UsersListResponse> GetAllAsync(
            long[] userIds,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<UsersListResponse>(
                $"{ResourceUri}/show_many?ids={ZendeskFormatter.ToCsv(userIds)}",
                "show-many-users",
                $"ListAsync({ZendeskFormatter.ToCsv(userIds)})",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<UserRelatedInformationResponse> GetRelatedInformationAsync(
            long userId,
            CancellationToken cancellationToken = default)
        {
            var response = await GetWithNotFoundCheckAsync<SingleUserRelatedInformationResponse>(
                $"{ResourceUri}/{userId}/related",
                "show-user-related-information",
                $"GetRelatedInformationAsync({userId})",
                $"User {userId} not found",
                cancellationToken: cancellationToken);

            return response?
                .UserRelatedInformationResponse;
        }

        public async Task<IncrementalUsersResponse<UserResponse>> GetIncrementalExport(
            DateTime startTime,
            CancellationToken cancellationToken = default)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var nextPage = Convert.ToInt64((startTime - epoch).TotalSeconds);

            return await GetAsync<IncrementalUsersResponse<UserResponse>>(
                $"{IncrementalResourceUri}/users?start_time={nextPage}",
                "incremental-user-export",
                $"GetIncrementalExport",
                cancellationToken: cancellationToken);
        }
        
        public async Task<UserResponse> CreateAsync(
            UserCreateRequest user,
            CancellationToken cancellationToken = default)
        {
            var response = await CreateAsync<SingleUserResponse, UserRequest<UserCreateRequest>>(
                ResourceUri,
                new UserRequest<UserCreateRequest>(user),
                "create-user",
                cancellationToken: cancellationToken);

            return response?
                .UserResponse;
        }
        
        public async Task<UserResponse> UpdateAsync(
            UserUpdateRequest user,
            CancellationToken cancellationToken = default)
        {
            var response = await UpdateWithNotFoundCheckAsync<SingleUserResponse, UserRequest<UserUpdateRequest>>(
                $"{ResourceUri}/{user.Id}",
                new UserRequest<UserUpdateRequest>(user),
                "update-user",
                $"Cannot update user as user {user.Id} cannot be found",
                cancellationToken: cancellationToken);

            return response?
                .UserResponse;
        }

        public async Task<JobStatusResponse> UpdateAsync(IEnumerable<UserUpdateRequest> users,
            CancellationToken cancellationToken = default)
        {
            var response = await UpdateAsync<SingleJobStatusResponse, UserListRequest<UserUpdateRequest>>(
                $"{ResourceUri}/update_many",
                new UserListRequest<UserUpdateRequest>(users),
                "update-many-users",
                "UpdateAsync",
                cancellationToken);
            
            return response.JobStatus;
        }

        public async Task<UserResponse> CreateOrUpdateAsync(
            UserCreateRequest user,
            CancellationToken cancellationToken = default)
        { 
           var response = await ExecuteRequest(async (client, token) => 
                       await client.PostAsJsonAsync(
                           $"{ResourceUri}/create_or_update",
                           new UserRequest<UserCreateRequest>(user),
                           cancellationToken: token)
                       .ConfigureAwait(false), 
                    "CreateOrUpdateAsync", 
                    cancellationToken)
                .ThrowIfUnsuccessful(
                    $"{DocsResource}#create-or-update-user",
                    new[] { HttpStatusCode.Created, HttpStatusCode.OK })
                .ReadContentAsAsync<SingleUserResponse>();

           return response?
               .UserResponse;
        }

        public async Task DeleteAsync(
            long userId,
            CancellationToken cancellationToken = default)
        {
            await DeleteAsync(
                ResourceUri,
                userId,
                "delete-user",
                HttpStatusCode.OK,
                cancellationToken: cancellationToken);
        }

        public async Task<JobStatusResponse> DeleteAsync(
            IEnumerable<long> userIds,
            CancellationToken cancellationToken = default)
        {
            var ids = userIds
                .ToList();

            var jobStatusResponse = await DeleteAsync<SingleJobStatusResponse>(
                $"{ResourceUri}/destroy_many.json",
                ids,
                "bulk-deleting-users",
                cancellationToken: cancellationToken);

            return jobStatusResponse?
                .JobStatus;
        }
    }
}
