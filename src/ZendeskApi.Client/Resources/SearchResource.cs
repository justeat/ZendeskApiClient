using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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

        private Func<ILogger, string, IDisposable> _loggerScope =
            LoggerMessage.DefineScope<string>(typeof(SearchResource).Name + ": {0}");

        public SearchResource(IZendeskApiClient apiClient,
            ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IPagination<SearchResult>> SearchAsync<T>(IZendeskQuery<T> zendeskQuery)
        {
            using (_loggerScope(_logger, "Search"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync($"{SearchUri}?{zendeskQuery.BuildQuery()}").ConfigureAwait(false);
                return await response.Content.ReadAsAsync<SearchResultsResponse>();
            }
        }
    }
}
