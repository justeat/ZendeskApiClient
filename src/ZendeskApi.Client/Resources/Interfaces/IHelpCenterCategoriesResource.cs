using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources.Interfaces
{
    public interface IHelpCenterCategoriesResource
    {
        Task<HelpCenterCategoryListResponse> GetAllAsync(
            string locale = null,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<HelpCenterCategory> GetAsync(
            long id,
            string locale = null,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}