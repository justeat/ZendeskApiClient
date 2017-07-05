using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
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
                        var obj = new ISearchResult[] {
                            new TicketResponse { Id = 1, Url = new Uri("https://company.zendesk.com/api/v2/tickets/1.json") },
                            new GroupResponse { Id = 2, Url = new Uri("https://company.zendesk.com/api/v2/groups/2.json") },
                            new Organization { Id = 3, Url = new Uri("https://company.zendesk.com/api/v2/organizations/3.json") },
                            new UserResponse { Id = 4, Url = new Uri("https://company.zendesk.com/api/v2/users/4.json") }
                        };

                        if (req.Query.ContainsKey("query") && !string.IsNullOrEmpty(req.Query["query"][0])) {
                            var query = req.Query["query"][0].Split(':');

                            if (query[1] == "ticket")
                            {
                                obj = obj.OfType<TicketResponse>().ToArray();
                            }
                        }

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new SearchResponse<ISearchResult>
                        {
                            Results = obj,
                            Count = obj.Length,
                            NextPage = new Uri("https://foo.zendesk.com/api/v2/search.json?query=\"type:GroupResponse hello\"&sort_by=created_at&sort_order=desc&page=2")
                        });
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

        public sealed override void RefreshClient(string resource)
        {
            _client = _server.CreateClient();
            _client.BaseAddress = new Uri($"http://localhost/{CreateResource(resource)}");
        }

        private string CreateResource(string resource)
        {
            resource = resource?.Trim('/');

            return resource != null ? resource + "/" : "";
        }

        public Uri BaseUri => Client.BaseAddress;

        public override void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}
