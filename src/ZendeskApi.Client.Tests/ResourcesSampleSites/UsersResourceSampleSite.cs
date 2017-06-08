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
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Tests
{
    public class UsersResourceSampleSite : SampleSite
    {
        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/users", (req, resp, routeData) =>
                    {
                        var obj1 = new Contracts.Models.User
                        {
                            Id = 1245,
                            Email = "Fu1@fu.com"
                        };

                        var obj2 = new Contracts.Models.User
                        {
                            Id = 1245,
                            Email = "Fu2@fu.com"
                        };

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        resp.WriteAsync(JsonConvert.SerializeObject(new UsersResponse { Item = new[] { obj1, obj2 } }));

                        return Task.CompletedTask;
                    })
                    .MapGet("api/v2/groups/456/users", (req, resp, routeData) =>
                    {
                        var obj1 = new Contracts.Models.User
                        {
                            Id = 523,
                            Email = "Fu1@fu.com"
                        };

                        var obj2 = new Contracts.Models.User
                        {
                            Id = 552,
                            Email = "Fu2@fu.com"
                        };

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        resp.WriteAsync(JsonConvert.SerializeObject(new UsersResponse { Item = new[] { obj1, obj2 } }));

                        return Task.CompletedTask;
                    })
                    .MapGet("api/v2/groups/456/users", (req, resp, routeData) =>
                    {
                        var obj1 = new Contracts.Models.User
                        {
                            Id = 523,
                            Email = "Fu1@fu.com"
                        };

                        var obj2 = new Contracts.Models.User
                        {
                            Id = 552,
                            Email = "Fu2@fu.com"
                        };

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        resp.WriteAsync(JsonConvert.SerializeObject(new UsersResponse { Item = new[] { obj1, obj2 } }));

                        return Task.CompletedTask;
                    })
                    .MapGet("api/v2/organizations/5002/users", (req, resp, routeData) =>
                    {
                        var obj1 = new Contracts.Models.User
                        {
                            Id = 34634,
                            Email = "Fu1@fu.com"
                        };

                        var obj2 = new Contracts.Models.User
                        {
                            Id = 2364,
                            Email = "Fu2@fu.com"
                        };

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        resp.WriteAsync(JsonConvert.SerializeObject(new UsersResponse { Item = new[] { obj1, obj2 } }));

                        return Task.CompletedTask;
                    })
                    .MapGet("api/v2/users/445", (req, resp, routeData) =>
                    {
                        var obj1 = new Contracts.Models.User
                        {
                            Id = 445,
                            Email = "found@fu.com"
                        };

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        resp.WriteAsync(JsonConvert.SerializeObject(new UserResponse { Item = obj1 }));

                        return Task.CompletedTask;
                    })
                    ;
            }
        }

        private readonly TestServer _server;

        private HttpClient _client;
        public override HttpClient Client => _client;

        public UsersResourceSampleSite(string resource)
        {
            var webhostbuilder = new WebHostBuilder();
            webhostbuilder
                .ConfigureServices(services => {
                    services.AddRouting();
                })
                .Configure(app =>
                {
                    app.UseRouter(MatchesRequest);
                });

            _server = new TestServer(webhostbuilder);
            _client = _server.CreateClient();

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
