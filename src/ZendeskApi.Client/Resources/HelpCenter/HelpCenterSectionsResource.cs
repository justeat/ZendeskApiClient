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

        public async Task<HelpCenterSectionListResponse> GetAllAsync(
            string locale,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<HelpCenterSectionListResponse>(
                $"{ResourceUri}/{locale}/sections",
                "list-sections",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<HelpCenterSectionListResponse> GetAllAsync(
            string locale,
            long categoryId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<HelpCenterSectionListResponse>(
                $"{ResourceUri}/{locale}/categories/{categoryId}/sections",
                "list-sections",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<HelpCenterSection> GetAsync(
            string locale,
            long id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await GetWithNotFoundCheckAsync<SingleHelpCenterSectionResponse>(
                $"{ResourceUri}/{locale}/sections/{id}",
                "show-job-status",
                $"GetAsync({locale}, {id})",
                $"Help center category {locale} {id} not found",
                cancellationToken: cancellationToken);

            return response?
                .Section;
        }
    }
}