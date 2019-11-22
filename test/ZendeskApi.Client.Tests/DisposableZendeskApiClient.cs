using System;
using System.Net.Http;
using ZendeskApi.Client.Tests.ResourcesSampleSites;

namespace ZendeskApi.Client.Tests
{
    internal class DisposableZendeskApiClient<TValue> : DisposableZendeskApiClient<State<TValue>, long, TValue>,
        IZendeskApiClient, IDisposable
        where TValue : class
    {
        public DisposableZendeskApiClient(Func<string, SampleSite<State<TValue>, long, TValue>> siteCreator)
            : base(siteCreator)
        { }
    }

    internal class DisposableZendeskApiClient<TState, TValue> : DisposableZendeskApiClient<TState, long, TValue>,
        IZendeskApiClient, IDisposable
        where TState : State<long, TValue>, new()
        where TValue : class
    {
        public DisposableZendeskApiClient(Func<string, SampleSite<TState, long, TValue>> siteCreator) 
            : base(siteCreator)
        { }
    }


    internal class DisposableZendeskApiClient<TState, TKey, TValue> : IZendeskApiClient, IDisposable
        where TState : State<TKey, TValue>, new()
        where TValue : class
    {
        private readonly Func<string, SampleSite<TState, TKey, TValue>> _siteCreator;

        public DisposableZendeskApiClient(Func<string, SampleSite<TState, TKey, TValue>> siteCreator)
        {
            _siteCreator = siteCreator;
        }

        private SampleSite<TState, TKey, TValue> _createdSite;

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

        public HttpClient CreateServiceStatusClient()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _createdSite?.Dispose();
        }
    }
}
