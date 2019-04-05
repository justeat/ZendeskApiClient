using System;
using System.Net.Http;
using ZendeskApi.Client.Tests.ResourcesSampleSites;

namespace ZendeskApi.Client.Tests
{
    public class DisposableZendeskApiClient : IZendeskApiClient, IDisposable
    {
        private readonly Func<string, SampleSite> _siteCreator;

        public DisposableZendeskApiClient(Func<string, SampleSite> siteCreator)
        {
            _siteCreator = siteCreator;
        }

        private SampleSite _createdSite;

        public HttpClient CreateClient(string resource = null)
        {
            if (_createdSite != null)
            {
                _createdSite.RefreshClient(resource);
                return _createdSite.Client;
            }

            _createdSite = _siteCreator(resource);
            return _createdSite.Client;
        }

        public void Dispose()
        {
            _createdSite?.Dispose();
        }
    }
}
