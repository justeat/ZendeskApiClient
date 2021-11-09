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
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        Task<UsersListResponse> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<ICursorPagination<UserResponse>> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default);
        #endregion

        #region Get Deleted Users
        Task<UserResponse> GetAsync(
            long userId,
            CancellationToken cancellationToken = default);
        #endregion

        #region Permanently Delete Users
        Task PermanentlyDeleteAsync(
            long userId,
            CancellationToken cancellationToken = default);
        #endregion
    }
}