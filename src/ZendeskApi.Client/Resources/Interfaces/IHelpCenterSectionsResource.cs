using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources.Interfaces
{
    public interface IHelpCenterSectionsResource
    {
        Task<HelpCenterSectionListResponse> GetAllAsync(
            string locale,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<HelpCenterSectionListResponse> GetAllAsync(
            string locale,
            long categoryId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<HelpCenterSection> GetAsync(
            string locale,
            long id,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}