using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    /// <summary>
    /// <see cref="https://developer.zendesk.com/rest_api/docs/core/users#list-deleted-users"/>
    /// </summary>
    public class DeletedUsersResource : AbstractBaseResource<DeletedUsersResource>,
        IDeletedUsersResource
    {
        private const string ResourceUri = "api/v2/deleted_users";

        public DeletedUsersResource(
            IZendeskApiClient apiClient, 
            ILogger logger)
            : base(apiClient, logger, "users")
        { }

        public async Task<UsersListResponse> ListAsync(PagerParameters pager = null)
        {
            return await GetAsync<UsersListResponse>(
                ResourceUri,
                "list-deleted-users",
                "ListAsync",
                pager);
        }

        public async Task<UserResponse> GetAsync(long userId)
        {
            var response = await GetWithNotFoundCheckAsync<SingleUserResponse>(
                $"{ResourceUri}/{userId}",
                "show-deleted-user",
                $"GetAsync({userId})",
                $"UserResponse {userId} not found");

            return response?
                .UserResponse;
        }

        public async Task PermanentlyDeleteAsync(long userId)
        {
            await DeleteAsync(
                ResourceUri,
                userId,
                "permanently-delete-user",
                HttpStatusCode.OK);
        }
    }
}
