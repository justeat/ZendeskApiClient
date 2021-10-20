using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources.Interfaces
{
    public interface IHelpCenterArticlesResource
    {
        Task<HelpCenterArticleListResponse> GetAllAsync(
            string locale = null,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<HelpCenterArticleListCursorResponse> GetAllAsync(
            CursorPager pager,
            string locale = null,
            CancellationToken cancellationToken = default);


        Task<HelpCenterArticleListResponse> GetAllByCategoryIdAsync(
            long categoryId,
            string locale = null,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<HelpCenterArticleListCursorResponse> GetAllByCategoryIdAsync(
            long categoryId,
            CursorPager pager,
            string locale = null,
            CancellationToken cancellationToken = default);

        Task<HelpCenterArticleListResponse> GetAllBySectionIdAsync(
            long sectionId,
            string locale = null,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<HelpCenterArticleListCursorResponse> GetAllBySectionIdAsync(
            long sectionId,
            CursorPager pager,
            string locale = null,
            CancellationToken cancellationToken = default);

        Task<HelpCenterArticleListResponse> GetAllByUserIdAsync(
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<HelpCenterArticleListCursorResponse> GetAllByUserIdAsync(
            long userId,
            CursorPager pager,
            CancellationToken cancellationToken = default);

        Task<HelpCenterArticle> GetAsync(
            long id,
            string locale = null,
            CancellationToken cancellationToken = default);
    }
}