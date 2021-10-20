using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources.Interfaces
{
    public interface IHelpCenterSectionsResource
    {
        Task<HelpCenterSectionListResponse> GetAllAsync(
            string locale = null,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<HelpCenterSectionListCursorResponse> GetAllAsync(
            CursorPager pager,
            string locale = null,
            CancellationToken cancellationToken = default);

        Task<HelpCenterSectionListResponse> GetAllAsync(
            long categoryId,
            string locale = null,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<HelpCenterSectionListCursorResponse> GetAllByCategoryIdAsync(
            CursorPager pager,
            long categoryId,
            string locale = null,
            CancellationToken cancellationToken = default);

        Task<HelpCenterSection> GetAsync(
            long id,
            string locale = null,
            CancellationToken cancellationToken = default);
    }
}