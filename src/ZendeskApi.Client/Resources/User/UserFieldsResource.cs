using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests.User;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class UserFieldsResource : AbstractBaseResource<UserFieldsResource>, 
        IUserFieldsResource
    {
        private const string ResourceUri = "api/v2/user_fields";

        public UserFieldsResource(
            IZendeskApiClient apiClient,
            ILogger logger)
            : base(apiClient, logger, "user_fields")
        { }

        public async Task<IPagination<UserField>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<UserFieldsResponse>(
                ResourceUri,
                "list-user-fields",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<UserField> GetAsync(
            long userFieldId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await GetWithNotFoundCheckAsync<UserFieldResponse>(
                $"{ResourceUri}/{userFieldId}",
                "show-user-field",
                $"GetAsync({userFieldId})",
                $"UserResponse Field {userFieldId} not found",
                cancellationToken: cancellationToken);

            return response?
                .UserField;
        }

        public async Task<UserField> CreateAsync(
            UserField userField,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await CreateAsync<UserFieldResponse, UserFieldCreateUpdateRequest>(
                ResourceUri,
                new UserFieldCreateUpdateRequest(userField),
                "create-user-field",
                cancellationToken: cancellationToken);

            return response?
                .UserField;
        }

        public async Task<UserField> UpdateAsync(
            UserField userField,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await UpdateWithNotFoundCheckAsync<UserFieldResponse, UserFieldCreateUpdateRequest>(
                $"{ResourceUri}/{userField.Id}",
                new UserFieldCreateUpdateRequest(userField),
                "update-user-field",
                $"Cannot update user field as user field {userField.Id} cannot be found",
                cancellationToken: cancellationToken);

            return response?
                .UserField;
        }

        public async Task DeleteAsync(
            long userFieldId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await DeleteAsync(
                ResourceUri,
                userFieldId,
                "delete-user-field",
                cancellationToken: cancellationToken);
        }
    }
}
