using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Converters;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Queries;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class SearchResource : ISearchResource
    {
        private const string SearchUri = "api/v2/search";

        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        private readonly Func<ILogger, string, IDisposable> _loggerScope = LoggerMessage.DefineScope<string>(typeof(SearchResource).Name + ": {0}");

        public SearchResource(IZendeskApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<SearchListResponse<ISearchResult>> SearchAsync(Action<IZendeskQuery> builder, PagerParameters pager = null)
        {
            var query = new ZendeskQuery();
            builder(query);

            using (_loggerScope(_logger, "SearchAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync($"{SearchUri}?{query.BuildQuery()}", pager).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<SearchListResponse<ISearchResult>>(new SearchJsonConverter());
            }
        }

        public async Task<SearchListResponse<T>> SearchAsync<T>(Action<IZendeskQuery> builder, PagerParameters pager = null) where T : ISearchResult
        {
            var query = new ZendeskQuery();
            builder(query);

            query.WithTypeFilter<T>();

            using (_loggerScope(_logger, "SearchAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync($"{SearchUri}?{query.BuildQuery()}", pager).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<SearchListResponse<T>>();
            }
        }
    }
}
