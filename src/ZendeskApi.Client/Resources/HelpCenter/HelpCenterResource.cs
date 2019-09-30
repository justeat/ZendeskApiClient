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

        private Lazy<IHelpCenterSectionsResource> SectionsLazy => new Lazy<IHelpCenterSectionsResource>(() => new HelpCenterSectionsResource(_apiClient, _logger));
        public IHelpCenterSectionsResource Sections => SectionsLazy.Value;

        private Lazy<IHelpCenterArticlesResource> ArticlesLazy => new Lazy<IHelpCenterArticlesResource>(() => new HelpCenterArticlesResource(_apiClient, _logger));
        public IHelpCenterArticlesResource Articles => ArticlesLazy.Value;

        private Lazy<IHelpCenterLocalesResource> LocalesLazy => new Lazy<IHelpCenterLocalesResource>(() => new HelpCenterLocalesResource(_apiClient, _logger));
        public IHelpCenterLocalesResource Locales => LocalesLazy.Value;
    }
}