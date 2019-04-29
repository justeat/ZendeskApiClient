using System;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IUsersResource
    {
        #region List Users
        Task<UsersListResponse> ListAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<UsersListResponse> ListAsync(
            long[] userIds, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<UsersListResponse> ListInGroupAsync(
            long groupId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<UsersListResponse> ListInOrganizationAsync(
            long organizationId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<UsersListResponse> ListByExternalIdsAsync(
            string[] externalIds, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        //Task<UserResponse> ListRelatedUsersAsync(long userId); //TODO: Fix this
        #endregion

        #region Get Users
        Task<UserResponse> GetAsync(
            long userId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<IncrementalUsersResponse<UserResponse>> GetIncrementalExport(
            DateTime startTime,
            CancellationToken cancellationToken = default(CancellationToken));
        #endregion

        #region Create Users
        Task<UserResponse> CreateAsync(
            UserCreateRequest user,
            CancellationToken cancellationToken = default(CancellationToken));
        #endregion

        #region Update Users
        Task<UserResponse> UpdateAsync(
            UserUpdateRequest user,
            CancellationToken cancellationToken = default(CancellationToken));
        #endregion

        #region Delete Users
        Task DeleteAsync(
            long userId,
            CancellationToken cancellationToken = default(CancellationToken));
        #endregion
    }
}