using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class UserIdentitiesResource : IUserIdentityResource
    {
        private const string ResourceUriFormat = "api/v2/users/{0}/identities";
        private const string EndUsersResourceUriFormat = "api/v2/end_users/{0}/identities";

        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        private Func<ILogger, string, IDisposable> _loggerScope =
            LoggerMessage.DefineScope<string>(typeof(UserIdentitiesResource).Name + ": {0}");

        public UserIdentitiesResource(IZendeskApiClient apiClient,
            ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IPagination<UserIdentity>> GetAllForUserAsync(long userId)
        {
            using (_loggerScope(_logger, $"GetAllForUserAsync({userId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(ResourceUriFormat, userId)).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<UserIdentitiesResponse>());
            }
        }

        public async Task<UserIdentity> GetIdentityForUserAsync(long userId, long identityId)
        {
            using (_loggerScope(_logger, $"GetIdentityForUserAsync({userId},{identityId})"))
            using (var client = _apiClient.CreateClient(string.Format(ResourceUriFormat, userId)))
            {
                var response = await client.GetAsync(identityId.ToString()).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Identity {0} for user {1} not found", userId, identityId);
                    return null;
                }

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<UserIdentityResponse>()).Item;
            }
        }

        public async Task<UserIdentity> CreateUserIdentityAsync(UserIdentity identity, long userId)
        {
            using (_loggerScope(_logger, $"CreateUserIdentityAsync({userId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(string.Format(ResourceUriFormat, userId), new UserIdentityRequest { Item = identity }).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 201 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/user_identities#create-identity");
                }

                return (await response.Content.ReadAsAsync<UserIdentityResponse>()).Item;
            }
        }

        public async Task<UserIdentity> CreateEndUserIdentityAsync(UserIdentity identity, long endUserId)
        {
            using (_loggerScope(_logger, $"CreateEndUserIdentityAsync({endUserId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(string.Format(EndUsersResourceUriFormat, endUserId), new UserIdentityRequest { Item = identity }).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 201 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/user_identities#create-identity");
                }

                return (await response.Content.ReadAsAsync<UserIdentityResponse>()).Item;
            }
        }

        public async Task<UserIdentity> PutAsync(UserIdentity identity)
        {
            using (_loggerScope(_logger, $"PutAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PutAsJsonAsync(string.Format(ResourceUriFormat, identity.UserId), new UserIdentityRequest { Item = identity }).ConfigureAwait(false);
                
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Cannot update identity as identity {0} cannot be found", identity.Id);
                    return null;
                }

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<UserIdentityResponse>()).Item;
            }
        }

        public async Task DeleteAsync(long userId, long identityId)
        {
            using (_loggerScope(_logger, $"DeleteAsync({userId}, {identityId})"))
            using (var client = _apiClient.CreateClient(string.Format(ResourceUriFormat, userId)))
            {
                var response = await client.DeleteAsync(identityId.ToString());

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 204 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/tickets#delete-ticket");
                }
            }
        }
    }
}
