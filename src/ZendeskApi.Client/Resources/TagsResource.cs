using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources.Interfaces;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TagsResource : AbstractBaseResource<TagsResource>, ITagsResource
    {
        private static string ResourceUri = "/api/v2/tags";
        public TagsResource(IZendeskApiClient apiClient, ILogger logger) : base(apiClient, logger, "tags")
        {
        }

        public async Task<ICursorPagination<Tag>> GetAllAsync(CursorPager cursor, CancellationToken cancellationToken = default)
        {
            return await GetAsync<TagResponse>(
               ResourceUri,
               "list-tags",
               "ListAsync",
               cursor,
               cancellationToken: cancellationToken);
        }
    }
}

