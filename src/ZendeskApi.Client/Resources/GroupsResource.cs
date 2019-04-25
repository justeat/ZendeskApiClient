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

        public async Task<GroupListResponse> ListAsync(PagerParameters pager = null)
        {
            return await GetAsync<GroupListResponse>(
                GroupsResourceUri,
                "list-groups",
                "ListAsync",
                pager);
        }

        public async Task<GroupListResponse> ListAsync(long userId, PagerParameters pager = null)
        {
            return await GetWithNotFoundCheckAsync<GroupListResponse>(
                string.Format(GroupsByUserResourceUriFormat, userId),
                "list-groups",
                $"ListAsync({userId})",
                $"UserResponse {userId} not found",
                pager);
        }

        public async Task<GroupListResponse> ListAssignableAsync(PagerParameters pager = null)
        {
            return await GetAsync<GroupListResponse>(
                AssignableGroupUri,
                "show-assignable-groups",
                "ListAssignableAsync",
                pager);
        }

        public async Task<Group> GetAsync(long groupId)
        {
            var response = await GetWithNotFoundCheckAsync<GroupResponse>(
                $"{GroupsResourceUri}/{groupId}",
                "show-group",
                $"GetAsync({groupId})",
                $"GroupResponse {groupId} not found");

            return response?
                .Group;
        }

        public async Task<Group> CreateAsync(GroupCreateRequest group)
        {
            var response = await CreateAsync<GroupResponse, GroupRequest<GroupCreateRequest>>(
                GroupsResourceUri,
                new GroupRequest<GroupCreateRequest>(group),
                "create-group"
            );

            return response?
                .Group;
        }

        public async Task<Group> UpdateAsync(GroupUpdateRequest group)
        {
            var response = await UpdateWithNotFoundCheckAsync<GroupResponse, GroupRequest<GroupUpdateRequest>>(
                $"{GroupsResourceUri}/{group.Id}",
                new GroupRequest<GroupUpdateRequest>(group),
                "update-group",
                $"Cannot update group as group {group.Id} cannot be found");

            return response?
                .Group;
        }

        public async Task DeleteAsync(long groupId)
        {
            await DeleteAsync(
                GroupsResourceUri,
                groupId,
                "delete-group");
        }
    }
}
