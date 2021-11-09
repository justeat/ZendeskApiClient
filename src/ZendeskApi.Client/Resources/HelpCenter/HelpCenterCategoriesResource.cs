using System;
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

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        public async Task<HelpCenterCategoryListResponse> GetAllAsync(
            string locale = null,
            PagerParameters pager = null, 
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<HelpCenterCategoryListResponse>(
                locale == null ? $"{ResourceUri}/categories" : $"{ResourceUri}/{locale}/categories",
                "list-categories",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<HelpCenterCategoryListCursorResponse> GetAllAsync(
            CursorPager pager,
            string locale = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<HelpCenterCategoryListCursorResponse>(
                locale == null ? $"{ResourceUri}/categories" : $"{ResourceUri}/{locale}/categories",
                "list-categories",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<HelpCenterCategory> GetAsync(
            long id,
            string locale = null,
            CancellationToken cancellationToken = default)
        {
            var response = await GetWithNotFoundCheckAsync<SingleHelpCenterCategoryResponse>(
                locale == null ? $"{ResourceUri}/categories/{id}" : $"{ResourceUri}/{locale}/categories/{id}",
                "show-job-status",
                $"GetAsync({locale}, {id})",
                $"Help center category {locale} {id} not found",
                cancellationToken: cancellationToken);

            return response?
                .Category;
        }
    }
}