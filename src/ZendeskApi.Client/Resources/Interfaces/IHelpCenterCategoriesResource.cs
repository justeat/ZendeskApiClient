using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources.Interfaces
{
    public interface IHelpCenterCategoriesResource
    {
        Task<HelpCenterCategoryListResponse> GetAllAsync(
            string locale,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<HelpCenterCategory> GetAsync(
            string locale,
            long id,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}