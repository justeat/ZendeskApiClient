using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class State<TKey, TValue>
        where TValue : class
    {
        public IDictionary<TKey, TValue> Items = new Dictionary<TKey, TValue>();
    }

    internal class State<TValue> : State<long, TValue>
        where TValue : class
    { }

    internal abstract class SampleSite<TValue> : SampleSite<State<TValue>, long, TValue>
        where TValue : class
    {
        protected SampleSite(
            string resource,
            Action<IRouteBuilder> matchesRequest,
            Action<WebHostBuilder> webHostBuilderSettings = null,
            Action<State<TValue>> populateState = null)
            : base(resource, matchesRequest, webHostBuilderSettings, populateState)
        { }
    }

    internal abstract class SampleSite<TState, TValue> : SampleSite<TState, long, TValue>
        where TState : State<TValue>, new()
        where TValue : class
    {
        protected SampleSite(
            string resource, 
            Action<IRouteBuilder> matchesRequest,
            Action<WebHostBuilder> webHostBuilderSettings = null, 
            Action<TState> populateState = null) 
            : base(resource, matchesRequest, webHostBuilderSettings, populateState)
        { }
    }

    internal abstract class SampleSite<TState, TKey, TValue> : IDisposable
        where TState : State<TKey, TValue>, new()
        where TValue : class
    {
        protected readonly TestServer Server;

        protected SampleSite(
            string resource,
            Action<IRouteBuilder> matchesRequest,
            Action<WebHostBuilder> webHostBuilderSettings = null,
            Action<TState> populateState = null)
        {
            var builder = new WebHostBuilder();

            var state = new TState();

            populateState?.Invoke(state);

            builder
                .ConfigureServices(services => {
                    services.AddSingleton(_ => state);
                    services.AddRouting();
                    services.AddMemoryCache();
                })
                .Configure(app =>
                {
                    app.UseRouter(matchesRequest);
                });

            webHostBuilderSettings?.Invoke(builder);

            Server = new TestServer(builder);

            RefreshClient(resource);
        }

        public static Random Rand = new Random();

        public Uri BaseUri => Client.BaseAddress;

        public HttpClient Client { get; private set; }

        public void RefreshClient(string resource)
        {
            Client = Server.CreateClient();
            Client.BaseAddress = new Uri($"http://localhost/{CreateResource(resource)}");
        }

        protected virtual string CreateResource(string resource)
        {
            var trimmedResource = resource?.Trim('/');
            return trimmedResource != null ? trimmedResource + "/" : "";
        }

        public virtual void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }
    }
}
