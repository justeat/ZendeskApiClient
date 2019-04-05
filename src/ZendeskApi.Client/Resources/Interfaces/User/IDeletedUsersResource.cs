using System;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IDeletedUsersResource
    {
        #region List Deleted Users
        Task<UsersListResponse> ListAsync(PagerParameters pager = null);
        #endregion

        #region Get Deleted Users
        Task<UserResponse> GetAsync(long userId);
        #endregion

        #region Permanently Delete Users
        Task PermanentlyDeleteAsync(long userId);
        #endregion
    }
}