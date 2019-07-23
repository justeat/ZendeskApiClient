using System;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Resources.Interfaces;

namespace ZendeskApi.Client.Resources
{
    public class HelpCenterResource : IHelpCenterResource
    {
        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        public HelpCenterResource(
            IZendeskApiClient apiClient,
            ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        private Lazy<IHelpCenterCategoriesResource> CategoriesLazy => new Lazy<IHelpCenterCategoriesResource>(() => new HelpCenterCategoriesResource(_apiClient, _logger));
        public IHelpCenterCategoriesResource Categories => CategoriesLazy.Value;
    }
}