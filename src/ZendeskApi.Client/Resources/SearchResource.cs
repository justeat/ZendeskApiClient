using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Converters;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Queries;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class SearchResource : AbstractBaseResource<SearchResource>,
        ISearchResource
    {
        private const string SearchUri = "api/v2/search";

        public SearchResource(
            IZendeskApiClient apiClient, 
            ILogger logger)
            : base(apiClient, logger, "search")
        { }

        public async Task<SearchResponse<ISearchResult>> SearchAsync(
            Action<IZendeskQuery> builder,
            PagerParameters pager = null, 
            CancellationToken cancellationToken = default)
        {
            var query = new ZendeskQuery();

            builder(query);

            return await GetAsync<SearchResponse<ISearchResult>>(
                $"{SearchUri}?{query.BuildQuery()}",
                "list-search-results",
                "SearchAsync",
                pager,
                new SearchJsonConverter(),
                cancellationToken);
        }

        public async Task<SearchResponse<T>> SearchAsync<T>(
            Action<IZendeskQuery> builder, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default) 
            where T : ISearchResult
        {
            var query = new ZendeskQuery();

            builder(query);

            query.WithTypeFilter<T>();

            return await GetAsync<SearchResponse<T>>(
                $"{SearchUri}?{query.BuildQuery()}",
                "list-search-results",
                "SearchAsync",
                pager,
                cancellationToken: cancellationToken);
        }
    }
}
