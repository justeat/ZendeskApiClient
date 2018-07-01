using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    public class DeletedUsersResourceSampleSite : SampleSite
    {
        private class State
        {
            public readonly IDictionary<long, UserResponse> DeletedUsers = new Dictionary<long, UserResponse>()
            {
                {
                    1,
                    new UserResponse()
                    {
                        Id = 1,
                        Name = "Kung Fu Wizard",
                        Email = "Fu1@fu.com"
                    }
                },
                { 
                    2,
                    new UserResponse()
                    {
                        Id = 2,
                        Name = "some name",
                        Email = "Fu2@fu.com"
                    }
                }
            };
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/deleted_users/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        if (!state.DeletedUsers.ContainsKey(id))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var user = state.DeletedUsers[id];

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new SingleUserResponse
                        {
                            UserResponse = user
                        });
                    })
                    .MapGet("api/v2/deleted_users", (req, resp, routeData) =>
                    {
                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new UsersListResponse
                        {
                            Users = state.DeletedUsers.Values
                        });
                    })
                    .MapDelete("api/v2/deleted_users/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        state.DeletedUsers.Remove(id);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return Task.CompletedTask;
                    });
            }
        }

        private readonly TestServer _server;

        private HttpClient _client;
        public override HttpClient Client => _client;

        public DeletedUsersResourceSampleSite(string resource)
        {
            var webhostbuilder = new WebHostBuilder();
            webhostbuilder
                .ConfigureServices(services =>
                {
                    services.AddSingleton(_ => new State());
                    services.AddSingleton(_ => new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<UserUpdateRequest, UserResponse>();
                        cfg.CreateMap<UserCreateRequest, UserResponse>();
                    }).CreateMapper());
                    services.AddRouting();
                    services.AddMemoryCache();
                })
                .Configure(app =>
                {
                    app.UseRouter(MatchesRequest);
                });

            _server = new TestServer(webhostbuilder);
            _client = _server.CreateClient();

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
