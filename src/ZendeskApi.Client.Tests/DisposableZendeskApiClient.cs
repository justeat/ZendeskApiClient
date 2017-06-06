using System;
using System.Net.Http;

namespace ZendeskApi.Client.Tests
{
    public class DisposableZendeskApiClient : IZendeskApiClient, IDisposable
    {
        private ZendeskSampleSite _sampleSite;

        public HttpClient CreateClient(string resource)
        {
            if (_sampleSite != null)
            {
                return _sampleSite.Client;
            }

            _sampleSite = new ZendeskSampleSite(resource);
            return _sampleSite.Client;
        }

        public void Dispose()
        {
            _sampleSite.Dispose();
        }
    }
}
