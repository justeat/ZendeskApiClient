using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class UserIdentitiesResource : AbstractBaseResource<UserIdentitiesResource>,
        IUserIdentityResource
    {
        private const string ResourceUriFormat = "api/v2/users/{0}/identities";
        private const string EndUsersResourceUriFormat = "api/v2/end_users/{0}/identities";

        public UserIdentitiesResource(
            IZendeskApiClient apiClient,
            ILogger logger)
            :base (apiClient, logger, "user_identities")
        { }

        [Obsolete("Use `GetAllByUserIdAsync` instead.")]
        public async Task<IPagination<UserIdentity>> GetAllForUserAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllByUserIdAsync(
                userId,
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        public async Task<IPagination<UserIdentity>> GetAllByUserIdAsync(
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<UserIdentitiesResponse>(
                string.Format(ResourceUriFormat, userId),
                "list-identities",
                $"GetAllByUserIdAsync({userId})",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<ICursorPagination<UserIdentity>> GetAllByUserIdAsync(
            long userId,
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<UserIdentitiesCursorResponse>(
                string.Format(ResourceUriFormat, userId),
                "list-identities",
                $"GetAllByUserIdAsync({userId})",
                pager,
                cancellationToken: cancellationToken);
        }

        [Obsolete("Use `GetIdentityByUserIdAsync` instead.")]
        public async Task<UserIdentity> GetIdentityForUserAsync(
            long userId, 
            long identityId,
            CancellationToken cancellationToken = default)
        {
            return await GetIdentityByUserIdAsync(
                userId,
                identityId,
                cancellationToken);
        }

        public async Task<UserIdentity> GetIdentityByUserIdAsync(
            long userId,
            long identityId,
            CancellationToken cancellationToken = default)
        {
            return (await GetWithNotFoundCheckAsync<UserIdentityResponse<UserIdentity>>(
                    $"{string.Format(ResourceUriFormat, userId)}/{identityId}",
                    "show-identity",
                    $"GetIdentityForUserAsync({userId},{identityId})",
                    $"Identity {identityId} for user {userId} not found",
                    cancellationToken: cancellationToken))
                ?.Identity;
        }

        public async Task<UserIdentity> CreateUserIdentityAsync(
            UserIdentity identity, 
            long userId,
            CancellationToken cancellationToken = default)
        {
            return (await CreateAsync<UserIdentityResponse<UserIdentity>, UserIdentityRequest<UserIdentity>>(
                    string.Format(ResourceUriFormat, userId),
                    new UserIdentityRequest<UserIdentity>(identity),
                    "create-identity",
                    scope: $"CreateUserIdentityAsync({userId})",
                    cancellationToken: cancellationToken))
                .Identity;
        }

        public async Task<UserIdentity> CreateEndUserIdentityAsync(
            UserIdentity identity, 
            long endUserId,
            CancellationToken cancellationToken = default)
        {
            return (await CreateAsync<UserIdentityResponse<UserIdentity>, UserIdentityRequest<UserIdentity>>(
                    string.Format(EndUsersResourceUriFormat, endUserId),
                    new UserIdentityRequest<UserIdentity>(identity),
                    "create-identity",
                    scope: $"CreateEndUserIdentityAsync({endUserId})",
                    cancellationToken: cancellationToken))
                .Identity;
        }

        public async Task<UserIdentity> UpdateAsync(
            UserIdentity identity,
            CancellationToken cancellationToken = default)
        {
            return (await UpdateWithNotFoundCheckAsync<UserIdentityResponse<UserIdentity>, UserIdentityRequest<UserIdentity>>(
                    $"{string.Format(ResourceUriFormat, identity.UserId)}/{identity.Id}",
                    new UserIdentityRequest<UserIdentity>(identity),
                    "update-identity",
                    $"Cannot update identity as identity {identity.Id} cannot be found",
                    cancellationToken: cancellationToken))
                ?.Identity;
        }

        public async Task DeleteAsync(
            long userId, 
            long identityId,
            CancellationToken cancellationToken = default)
        {
            await DeleteAsync(
                string.Format(ResourceUriFormat, userId),
                identityId,
                "delete-identity",
                scope: $"DeleteAsync({userId}, {identityId})",
                cancellationToken: cancellationToken);
        }
    }
}
