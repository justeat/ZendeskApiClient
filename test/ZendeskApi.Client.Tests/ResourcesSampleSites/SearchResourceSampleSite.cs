using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.ResourcesSampleSites;

namespace ZendeskApi.Client.Tests
{
    public class SearchResourceSampleSite : SampleSite
    {
        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/search", (req, resp, routeData) =>
                    {
                        var obj = new ISearchResult[] { new Ticket { Id = 1 }, new Group { Id = 2 }, new Organization { Id = 3 }, new User { Id = 4 }};

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new SearchResultsResponse { Item = obj, Count = obj.Length });
                    });
            }
        }

        private readonly TestServer _server;

        private HttpClient _client;
        public override HttpClient Client => _client;

        public SearchResourceSampleSite(string resource)
        {
            var webhostbuilder = new WebHostBuilder();
            webhostbuilder
                .ConfigureServices(services => {
                    services.AddRouting();
                    services.AddMemoryCache();
                })
                .Configure(app =>
                {
                    app.UseRouter(MatchesRequest);
                });

            _server = new TestServer(webhostbuilder);

            RefreshClient(resource);
        }

        public override void RefreshClient(string resource)
        {
            _client = _server.CreateClient();
            _client.BaseAddress = new Uri($"http://localhost/{CreateResource(resource)}");
        }

        private string CreateResource(string resource)
        {
            resource = resource?.Trim('/');

            return resource != null ? resource + "/" : resource;
        }

        public Uri BaseUri
        {
            get { return Client.BaseAddress; }
        }

        public override void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}
