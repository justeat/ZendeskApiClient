using System;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources.Interfaces
{
    public interface IHelpCenterCategoriesResource
    {
        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        Task<HelpCenterCategoryListResponse> GetAllAsync(
            string locale = null,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<HelpCenterCategoryListCursorResponse> GetAllAsync(
            CursorPager pager,
            string locale = null,
            CancellationToken cancellationToken = default);

        Task<HelpCenterCategory> GetAsync(
            long id,
            string locale = null,
            CancellationToken cancellationToken = default);
    }
}