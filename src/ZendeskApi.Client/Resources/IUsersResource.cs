using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUsersResource
    {
        #region List Users
        Task<UsersListResponse> ListAsync(PagerParameters pager = null);
        Task<UsersListResponse> ListAsync(long[] userIds, PagerParameters pager = null);
        Task<UsersListResponse> ListInGroupAsync(long groupId, PagerParameters pager = null);
        Task<UsersListResponse> ListInOrganizationAsync(long organizationId, PagerParameters pager = null);
        Task<UsersListResponse> ListByExternalIdsAsync(string[] externalIds, PagerParameters pager = null);
        //Task<UserResponse> ListRelatedUsersAsync(long userId); //TODO: Fix this
        #endregion

        #region Get Users
        Task<UserResponse> GetAsync(long userId);
        #endregion

        #region Create Users
        Task<UserResponse> CreateAsync(UserCreateRequest user);
        #endregion

        #region Update Users
        Task<UserResponse> UpdateAsync(UserUpdateRequest user);
        #endregion

        #region Delete Users
        Task DeleteAsync(long userId);
        #endregion
    }
}