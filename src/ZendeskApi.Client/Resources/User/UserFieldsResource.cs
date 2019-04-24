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

        public async Task<IPagination<UserField>> GetAllAsync(PagerParameters pager = null)
        {
            return await GetAsync<UserFieldsResponse>(
                ResourceUri,
                "list-user-fields",
                "GetAllAsync",
                pager);
        }

        public async Task<UserField> GetAsync(long userFieldId)
        {
            var response = await GetWithNotFoundCheckAsync<UserFieldResponse>(
                $"{ResourceUri}/{userFieldId}",
                "show-user-field",
                $"GetAsync({userFieldId})",
                $"UserResponse Field {userFieldId} not found");

            return response?
                .UserField;
        }

        public async Task<UserField> CreateAsync(UserField userField)
        {
            var response = await CreateAsync<UserFieldResponse, UserFieldCreateUpdateRequest>(
                ResourceUri,
                new UserFieldCreateUpdateRequest(userField),
                "create-user-field");

            return response?
                .UserField;
        }

        public async Task<UserField> UpdateAsync(UserField userField)
        {
            var response = await UpdateWithNotFoundCheckAsync<UserFieldResponse, UserFieldCreateUpdateRequest>(
                $"{ResourceUri}/{userField.Id}",
                new UserFieldCreateUpdateRequest(userField),
                "update-user-field",
                $"Cannot update user field as user field {userField.Id} cannot be found");

            return response?
                .UserField;
        }

        public async Task DeleteAsync(long userFieldId)
        {
            await DeleteAsync(
                ResourceUri,
                userFieldId,
                "delete-user-field");
        }
    }
}
