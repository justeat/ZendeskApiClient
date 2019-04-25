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

        public async Task<IPagination<UserIdentity>> GetAllForUserAsync(long userId, PagerParameters pager = null)
        {
            return await GetAsync<UserIdentitiesResponse>(
                string.Format(ResourceUriFormat, userId),
                "list-identities",
                $"GetAllForUserAsync({userId})",
                pager);
        }

        public async Task<UserIdentity> GetIdentityForUserAsync(long userId, long identityId)
        {
            return await GetWithNotFoundCheckAsync<UserIdentity>(
                $"{string.Format(ResourceUriFormat, userId)}/{identityId}",
                "show-identity",
                $"GetIdentityForUserAsync({userId},{identityId})",
                $"Identity {identityId} for user {userId} not found");
        }

        public async Task<UserIdentity> CreateUserIdentityAsync(UserIdentity identity, long userId)
        {
            return await CreateAsync<UserIdentity, UserIdentity>(
                string.Format(ResourceUriFormat, userId),
                identity,
                "create-identity",
                scope: $"CreateUserIdentityAsync({userId})");
        }

        public async Task<UserIdentity> CreateEndUserIdentityAsync(UserIdentity identity, long endUserId)
        {
            return await CreateAsync<UserIdentity, UserIdentity>(
                string.Format(EndUsersResourceUriFormat, endUserId),
                identity,
                "create-identity",
                scope: $"CreateEndUserIdentityAsync({endUserId})");
        }

        public async Task<UserIdentity> UpdateAsync(UserIdentity identity)
        {
            return await UpdateWithNotFoundCheckAsync<UserIdentity, UserIdentity>(
                string.Format(ResourceUriFormat, identity.UserId),
                identity,
                "update-identity",
                $"Cannot update identity as identity {identity.Id} cannot be found");
        }

        public async Task DeleteAsync(long userId, long identityId)
        {
            await DeleteAsync(
                string.Format(ResourceUriFormat, userId),
                identityId,
                "delete-identity",
                scope: $"DeleteAsync({userId}, {identityId})");
        }
    }
}
