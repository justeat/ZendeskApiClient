using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    class UserFieldsResource : IUserFieldsResource
    {
        private const string ResourceUri = "api/v2/user_fields";

        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        private Func<ILogger, string, IDisposable> _loggerScope =
            LoggerMessage.DefineScope<string>(typeof(UserFieldsResource).Name + ": {0}");

        public UserFieldsResource(IZendeskApiClient apiClient,
           ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IPagination<UserField>> GetAllAsync(PagerParameters pager = null)
        {
            using (_loggerScope(_logger, "GetAllAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(ResourceUri).ConfigureAwait(false);

                await response.ThrowIfUnsuccessful("user_fields#list-user-fields");

                return await response.Content.ReadAsAsync<UserFieldsResponse>();
            }
        }

        public async Task<UserField> GetAsync(long userFieldId)
        {
            using (_loggerScope(_logger, $"GetAsync({userFieldId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync(userFieldId.ToString()).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("UserResponse Field {0} not found", userFieldId);
                    return null;
                }

                await response.ThrowIfUnsuccessful("user_fields#show-user-field");

                var singleResponse = await response.Content.ReadAsAsync<UserFieldResponse>();
                return singleResponse.UserField;
            }
        }

        public async Task<UserField> CreateAsync(UserField userField)
        {
            using (_loggerScope(_logger, $"PostAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(ResourceUri, userField).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    await response.ThrowZendeskRequestException(
                        "user_fields#create-user-field",
                        System.Net.HttpStatusCode.Created);
                }

                return await response.Content.ReadAsAsync<UserField>();
            }
        }

        public async Task<UserField> UpdateAsync(UserField userField)
        {
            using (_loggerScope(_logger, $"PutAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PutAsJsonAsync(userField.Id.ToString(), userField).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Cannot update user field as user field {0} cannot be found", userField.Id);
                    return null;
                }

                await response.ThrowIfUnsuccessful("user_fields#update-user-fields");

                return await response.Content.ReadAsAsync<UserField>();
            }
        }

        public async Task DeleteAsync(long userFieldId)
        {
            using (_loggerScope(_logger, $"DeleteAsync({userFieldId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.DeleteAsync(userFieldId.ToString()).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    await response.ThrowZendeskRequestException(
                        "user_fields#delete-user-field",
                        System.Net.HttpStatusCode.NoContent);
                }
            }
        }

    }
}
