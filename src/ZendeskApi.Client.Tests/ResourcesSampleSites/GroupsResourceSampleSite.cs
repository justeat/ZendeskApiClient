using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ZendeskApi.Client.Tests.ResourcesSampleSites;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Tests
{
    public class GroupsResourceSampleSite : SampleSite
    {
        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/groups", (req, resp, routeData) =>
                    {
                        var group1 = new Group
                        {
                            Id = 211L,
                            Name = "DJs",
                            Created = DateTime.Parse("2009-05-13T00:07:08Z"),
                            Updated = DateTime.Parse("2011-07-22T00:11:12Z"),
                        };

                        var group2 = new Group
                        {
                            Id = 122L,
                            Name = "MCs",
                            Created = DateTime.Parse("2009-08-26T00:07:08Z"),
                            Updated = DateTime.Parse("2010-05-13T00:07:08Z"),
                        };

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        resp.WriteAsync(JsonConvert.SerializeObject(new GroupsResponse { Item = new[] { group1, group2 } }));

                        return Task.CompletedTask;
                    })
                    .MapGet("api/v2/users/123/groups", (req, resp, routeData) =>
                    {
                        var group1 = new Group
                        {
                            Id = 321L,
                            Name = "Group For 123",
                            Created = DateTime.Parse("2009-05-13T00:07:08Z"),
                            Updated = DateTime.Parse("2011-07-22T00:11:12Z"),
                        };

                        var group2 = new Group
                        {
                            Id = 342L,
                            Name = "Group For 123",
                            Created = DateTime.Parse("2009-08-26T00:07:08Z"),
                            Updated = DateTime.Parse("2010-05-13T00:07:08Z"),
                        };

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        resp.WriteAsync(JsonConvert.SerializeObject(new GroupsResponse { Item = new[] { group1, group2 } }));

                        return Task.CompletedTask;
                    }).
                    MapGet("api/v2/groups/assignable", (req, resp, routeData) =>
                    {
                        var group1 = new Group
                        {
                            Id = 321L,
                            Name = "Group For 123",
                            Created = DateTime.Parse("2009-05-13T00:07:08Z"),
                            Updated = DateTime.Parse("2011-07-22T00:11:12Z"),
                        };

                        var group2 = new Group
                        {
                            Id = 122L,
                            Name = "MCs",
                            Created = DateTime.Parse("2009-08-26T00:07:08Z"),
                            Updated = DateTime.Parse("2010-05-13T00:07:08Z"),
                        };

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        resp.WriteAsync(JsonConvert.SerializeObject(new GroupsResponse { Item = new[] { group1, group2 } }));

                        return Task.CompletedTask;
                    })
                    .MapGet("api/v2/groups/1", (req, resp, routeData) =>
                    {
                        var group1 = new Group
                        {
                            Id = 1L,
                            Name = "DJs",
                            Created = DateTime.Parse("2009-05-13T00:07:08Z"),
                            Updated = DateTime.Parse("2011-07-22T00:11:12Z"),
                        };

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        resp.WriteAsync(JsonConvert.SerializeObject(new GroupResponse { Item = group1 }));

                        return Task.CompletedTask;
                    })
                    .MapGet("api/v2/groups/14", (req, resp, routeData) =>
                    {
                        var group1 = new Group
                        {
                            Id = 14L,
                            Name = "DJs",
                            Created = DateTime.Parse("2009-05-13T00:07:08Z"),
                            Updated = DateTime.Parse("2011-07-22T00:11:12Z"),
                        };

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        resp.WriteAsync(JsonConvert.SerializeObject(new GroupResponse { Item = group1 }));

                        return Task.CompletedTask;
                    })
                    .MapPost("api/v2/groups", (req, resp, routeData) =>
                    {
                        var group = req.Body.Deserialize<GroupRequest>().Item;
                        
                        if (group.Name != null && group.Name.Contains("error"))
                        {
                            resp.StatusCode = (int)HttpStatusCode.PaymentRequired; // It doesnt matter as long as not 201

                            return Task.CompletedTask;
                        }

                        group.Id = long.Parse(new Random().Next().ToString());

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        resp.WriteAsync(JsonConvert.SerializeObject(new GroupResponse { Item = group }));

                        return Task.CompletedTask;
                    })
                    .MapPut("api/v2/groups/213", (req, resp, routeData) =>
                    {
                        var ticket = req.Body.Deserialize<GroupRequest>().Item;

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        resp.WriteAsync(JsonConvert.SerializeObject(new GroupResponse { Item = ticket }));

                        return Task.CompletedTask;
                    });
            }
        }

        private readonly TestServer _server;

        public override HttpClient Client { get; }

        public GroupsResourceSampleSite(string resource)
        {
            var webhostbuilder = new WebHostBuilder();
            webhostbuilder
                .ConfigureServices(services => services.AddRouting())
                .Configure(app =>
                {
                    app.UseRouter(MatchesRequest);
                });

            _server = new TestServer(webhostbuilder);
            Client = _server.CreateClient();

            resource = resource?.Trim('/');

            if (resource != null)
            {
                resource = resource + "/";
            }

            Client.BaseAddress = new Uri($"http://localhost/{resource}");
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
