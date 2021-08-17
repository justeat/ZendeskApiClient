using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources.Interfaces;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class HelpCenterSectionsResource : AbstractBaseResource<HelpCenterSectionsResource>,
        IHelpCenterSectionsResource
    {
        private const string ResourceUri = "api/v2/help_center";

        public HelpCenterSectionsResource(
            IZendeskApiClient apiClient,
            ILogger logger)
            : base(apiClient, logger, "help_center/sections")
        { }

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        public async Task<HelpCenterSectionListResponse> GetAllAsync(
            string locale = null,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<HelpCenterSectionListResponse>(
                locale == null ? $"{ResourceUri}/sections" : $"{ResourceUri}/{locale}/sections",
                "list-sections",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<HelpCenterSectionListCursorResponse> GetAllAsync(
            CursorPager pager,
            string locale = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<HelpCenterSectionListCursorResponse>(
                locale == null ? $"{ResourceUri}/sections" : $"{ResourceUri}/{locale}/sections",
                "list-sections",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        // Potential naming inconsistency here, should be: GetAllByCategoryIdAsync
        public async Task<HelpCenterSectionListResponse> GetAllAsync(
            long categoryId,
            string locale = null,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<HelpCenterSectionListResponse>(
                locale == null ? $"{ResourceUri}/categories/{categoryId}/sections" :$"{ResourceUri}/{locale}/categories/{categoryId}/sections",
                "list-sections",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        // Potential naming inconsistency here, should be: GetAllByCategoryIdAsync
        public async Task<HelpCenterSectionListCursorResponse> GetAllAsync(
            CursorPager pager,
            long categoryId,
            string locale = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<HelpCenterSectionListCursorResponse>(
                locale == null ? $"{ResourceUri}/categories/{categoryId}/sections" : $"{ResourceUri}/{locale}/categories/{categoryId}/sections",
                "list-sections",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<HelpCenterSection> GetAsync(
            long id,
            string locale = null,
            CancellationToken cancellationToken = default)
        {
            var response = await GetWithNotFoundCheckAsync<SingleHelpCenterSectionResponse>(
                locale == null ? $"{ResourceUri}/sections/{id}" : $"{ResourceUri}/{locale}/sections/{id}",
                "show-job-status",
                $"GetAsync({locale}, {id})",
                $"Help center category {locale} {id} not found",
                cancellationToken: cancellationToken);

            return response?
                .Section;
        }
    }
}