using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources.Interfaces;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Responses.Articles;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources.Articles
{
    public class ArticlesResources : IArticlesResources
    {
        private const string ResourceUri = "api/v2/help_center/articles/";

        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        private readonly Func<ILogger, string, IDisposable> _loggerScope = LoggerMessage.DefineScope<string>(typeof(ArticlesResources).Name + ": {0}");

        public ArticlesResources(IZendeskApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }


        public async System.Threading.Tasks.Task<Responses.IPagination<Article>> ListAsync(string query, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, "ListAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(ResourceUri + "search.json?query=" + query, pager).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                                .WithResponse(response)
                                .WithHelpDocsLink("support/articles#list-articles")
                                .Build();
                }

                var data = await response.Content.ReadAsAsync<SearchResponse<Article>>();
                return data;
            }
        }
    }
}
