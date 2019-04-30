using System;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IDeletedUsersResource
    {
        #region List Deleted Users
        [Obsolete("Use `GetAllAsync` instead.")]
        Task<UsersListResponse> ListAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<UsersListResponse> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));
        #endregion

        #region Get Deleted Users
        Task<UserResponse> GetAsync(
            long userId,
            CancellationToken cancellationToken = default(CancellationToken));
        #endregion

        #region Permanently Delete Users
        Task PermanentlyDeleteAsync(
            long userId,
            CancellationToken cancellationToken = default(CancellationToken));
        #endregion
    }
}