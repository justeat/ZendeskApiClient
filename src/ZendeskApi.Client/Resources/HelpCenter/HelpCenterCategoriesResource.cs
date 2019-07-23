using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources.Interfaces;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class HelpCenterCategoriesResource : AbstractBaseResource<HelpCenterCategoriesResource>,
        IHelpCenterCategoriesResource
    {
        private const string ResourceUri = "api/v2/help_center";

        public HelpCenterCategoriesResource(
            IZendeskApiClient apiClient,
            ILogger logger)
            : base(apiClient, logger, "help_center/categories")
        { }

        public async Task<HelpCenterCategoryListResponse> GetAllAsync(
            string locale,
            PagerParameters pager = null, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<HelpCenterCategoryListResponse>(
                $"{ResourceUri}/{locale}/categories",
                "list-categories",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<HelpCenterCategory> GetAsync(
            string locale, 
            long id, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await GetWithNotFoundCheckAsync<SingleHelpCenterCategoryResponse>(
                $"{ResourceUri}/{locale}/categories/{id}",
                "show-job-status",
                $"GetAsync({locale}, {id})",
                $"Help center category {locale} {id} not found",
                cancellationToken: cancellationToken);

            return response?
                .Category;
        }
    }
}