using System;
using System.Threading;
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
    public class GroupsResource : AbstractBaseResource<GroupsResource>, 
        IGroupsResource
    {
        private const string GroupsResourceUri = "api/v2/groups";
        private const string GroupsByUserResourceUriFormat = "api/v2/users/{0}/groups";
        private const string AssignableGroupUri = @"api/v2/groups/assignable";

        public GroupsResource(
            IZendeskApiClient apiClient,
            ILogger logger)
            : base(apiClient, logger, "groups")
        { }

        [Obsolete("Use `GetAllAsync` instead.")]
        public async Task<GroupListResponse> ListAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllAsync(
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllByUserIdAsync` instead.")]
        public async Task<GroupListResponse> ListAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllByUserIdAsync(
                userId,
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllByAssignableAsync` instead.")]
        public async Task<GroupListResponse> ListAssignableAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllByAssignableAsync(
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        public async Task<GroupListResponse> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<GroupListResponse>(
                GroupsResourceUri,
                "list-groups",
                "ListAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<GroupListCursorResponse> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<GroupListCursorResponse>(
                GroupsResourceUri,
                "list-groups",
                "ListAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<GroupListResponse> GetAllByUserIdAsync(
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetWithNotFoundCheckAsync<GroupListResponse>(
                string.Format(GroupsByUserResourceUriFormat, userId),
                "list-groups",
                $"ListAsync({userId})",
                $"UserResponse {userId} not found",
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllByAssignableAsync` with CursorPager parameter instead.")]
        public async Task<GroupListResponse> GetAllByAssignableAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<GroupListResponse>(
                AssignableGroupUri,
                "show-assignable-groups",
                "ListAssignableAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<GroupListCursorResponse> GetAllByAssignableAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<GroupListCursorResponse>(
                AssignableGroupUri,
                "show-assignable-groups",
                "ListAssignableAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<Group> GetAsync(
            long groupId,
            CancellationToken cancellationToken = default)
        {
            var response = await GetWithNotFoundCheckAsync<GroupResponse>(
                $"{GroupsResourceUri}/{groupId}",
                "show-group",
                $"GetAsync({groupId})",
                $"GroupResponse {groupId} not found",
                cancellationToken: cancellationToken);

            return response?
                .Group;
        }

        public async Task<Group> CreateAsync(
            GroupCreateRequest group,
            CancellationToken cancellationToken = default)
        {
            var response = await CreateAsync<GroupResponse, GroupRequest<GroupCreateRequest>>(
                GroupsResourceUri,
                new GroupRequest<GroupCreateRequest>(group),
                "create-group",
                cancellationToken: cancellationToken
            );

            return response?
                .Group;
        }

        public async Task<Group> UpdateAsync(
            GroupUpdateRequest group,
            CancellationToken cancellationToken = default)
        {
            var response = await UpdateWithNotFoundCheckAsync<GroupResponse, GroupRequest<GroupUpdateRequest>>(
                $"{GroupsResourceUri}/{group.Id}",
                new GroupRequest<GroupUpdateRequest>(group),
                "update-group",
                $"Cannot update group as group {group.Id} cannot be found",
                cancellationToken: cancellationToken);

            return response?
                .Group;
        }

        public async Task DeleteAsync(
            long groupId,
            CancellationToken cancellationToken = default)
        {
            await DeleteAsync(
                GroupsResourceUri,
                groupId,
                "delete-group",
                cancellationToken: cancellationToken);
        }
    }
}
