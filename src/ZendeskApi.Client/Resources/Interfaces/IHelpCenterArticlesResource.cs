using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources.Interfaces
{
    public interface IHelpCenterArticlesResource
    {
        Task<HelpCenterArticleListResponse> GetAllAsync(
            string locale,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<HelpCenterArticleListResponse> GetAllByCategoryIdAsync(
            string locale,
            long categoryId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<HelpCenterArticleListResponse> GetAllBySectionIdAsync(
            string locale,
            long sectionId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<HelpCenterArticleListResponse> GetAllByUserIdAsync(
            string locale,
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<HelpCenterArticle> GetAsync(
            string locale,
            long id,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}