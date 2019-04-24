using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Routing;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class SearchResourceSampleSite : SampleSite<ISearchResult>
    {
        public SearchResourceSampleSite(string resource)
            : base(resource, MatchesRequest)
        { }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/search", (req, resp, routeData) =>
                    {
                        var obj = new ISearchResult[] {
                            new Ticket { Id = 1, Url = new Uri("https://company.zendesk.com/api/v2/tickets/1.json") },
                            new Group { Id = 2, Url = new Uri("https://company.zendesk.com/api/v2/groups/2.json") },
                            new Organization { Id = 3, Url = new Uri("https://company.zendesk.com/api/v2/organizations/3.json") },
                            new UserResponse { Id = 4, Url = new Uri("https://company.zendesk.com/api/v2/users/4.json") }
                        };

                        if (req.Query.ContainsKey("query") && !string.IsNullOrEmpty(req.Query["query"][0])) {
                            var query = req.Query["query"][0].Split(':');

                            if (query[1] == "ticket")
                            {
                                obj = obj.OfType<Ticket>().ToArray();
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
    }
}
