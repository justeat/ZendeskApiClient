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

        [Obsolete("Use `SearchAsync` with CursorPager parameter instead.")]
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

        public async Task<SearchCursorResponse<ISearchResult>> SearchAsync(
            Action<IZendeskQuery> builder,
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            var query = new ZendeskQuery();

            builder(query);

            return await GetAsync<SearchCursorResponse<ISearchResult>>(
                $"{SearchUri}?{query.BuildQuery()}",
                "list-search-results",
                "SearchAsync",
                pager,
                new SearchJsonCursorConverter(),
                cancellationToken);
        }

        [Obsolete("Use `SearchAsync` with CursorPager parameter instead.")]
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

        public async Task<SearchCursorResponse<T>> SearchAsync<T>(
            Action<IZendeskQuery> builder,
            CursorPager pager,
            CancellationToken cancellationToken = default)
            where T : ISearchResult
        {
            var query = new ZendeskQuery();

            builder(query);

            query.WithTypeFilter<T>();

            return await GetAsync<SearchCursorResponse<T>>(
                $"{SearchUri}?{query.BuildQuery()}",
                "list-search-results",
                "SearchAsync",
                pager,
                cancellationToken: cancellationToken);
        }
    }
}
