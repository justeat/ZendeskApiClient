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
            CancellationToken cancellationToken = default(CancellationToken));

        Task<HelpCenterSection> GetAsync(
            long id,
            string locale = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<HelpCenterSectionListResponse> GetAllAsync(
            long categoryId,
            string locale = null,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}