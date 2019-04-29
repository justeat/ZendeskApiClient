using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
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

        public async Task<IPagination<UserIdentity>> GetAllForUserAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<UserIdentitiesResponse>(
                string.Format(ResourceUriFormat, userId),
                "list-identities",
                $"GetAllForUserAsync({userId})",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<UserIdentity> GetIdentityForUserAsync(
            long userId, 
            long identityId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetWithNotFoundCheckAsync<UserIdentity>(
                $"{string.Format(ResourceUriFormat, userId)}/{identityId}",
                "show-identity",
                $"GetIdentityForUserAsync({userId},{identityId})",
                $"Identity {identityId} for user {userId} not found",
                cancellationToken: cancellationToken);
        }

        public async Task<UserIdentity> CreateUserIdentityAsync(
            UserIdentity identity, 
            long userId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await CreateAsync<UserIdentity, UserIdentity>(
                string.Format(ResourceUriFormat, userId),
                identity,
                "create-identity",
                scope: $"CreateUserIdentityAsync({userId})",
                cancellationToken: cancellationToken);
        }

        public async Task<UserIdentity> CreateEndUserIdentityAsync(
            UserIdentity identity, 
            long endUserId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await CreateAsync<UserIdentity, UserIdentity>(
                string.Format(EndUsersResourceUriFormat, endUserId),
                identity,
                "create-identity",
                scope: $"CreateEndUserIdentityAsync({endUserId})",
                cancellationToken: cancellationToken);
        }

        public async Task<UserIdentity> UpdateAsync(
            UserIdentity identity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await UpdateWithNotFoundCheckAsync<UserIdentity, UserIdentity>(
                $"{string.Format(ResourceUriFormat, identity.UserId)}/{identity.Id}",
                identity,
                "update-identity",
                $"Cannot update identity as identity {identity.Id} cannot be found",
                cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(
            long userId, 
            long identityId,
            CancellationToken cancellationToken = default(CancellationToken))
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
