using System;
using System.Net;
using System.Threading;
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

        [Obsolete("Use `GetAllAsync` instead.")]
        public async Task<UsersListResponse> ListAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAllAsync(
                pager,
                cancellationToken);
        }

        public async Task<UsersListResponse> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<UsersListResponse>(
                ResourceUri,
                "list-deleted-users",
                "ListAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<UserResponse> GetAsync(
            long userId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await GetWithNotFoundCheckAsync<SingleUserResponse>(
                $"{ResourceUri}/{userId}",
                "show-deleted-user",
                $"GetAsync({userId})",
                $"UserResponse {userId} not found",
                cancellationToken: cancellationToken);

            return response?
                .UserResponse;
        }

        public async Task PermanentlyDeleteAsync(
            long userId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await DeleteAsync(
                ResourceUri,
                userId,
                "permanently-delete-user",
                HttpStatusCode.OK,
                cancellationToken: cancellationToken);
        }
    }
}
