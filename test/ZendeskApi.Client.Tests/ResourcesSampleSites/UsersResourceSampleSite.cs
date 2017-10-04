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
    public class UsersResourceSampleSite : SampleSite
    {
        private class State
        {
            public readonly IDictionary<long, UserResponse> Users = new Dictionary<long, UserResponse>();
        }
        
        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/users/show_many", (req, resp, routeData) =>
                    {
                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var users = Enumerable.Empty<UserResponse>();

                        if (req.Query.ContainsKey("ids"))
                        {
                            var ids = req.Query["ids"].ToString().Split(',').Select(long.Parse);
                            users = state.Users.Where(x => ids.Contains(x.Key)).Select(p => p.Value);
                        }

                        if (req.Query.ContainsKey("external_ids"))
                        {
                            var ids = req.Query["external_ids"].ToString().Split(',')
                                .Where(x => !string.IsNullOrWhiteSpace(x));
                            users = state.Users.Where(x => ids.Contains(x.Value.ExternalId)).Select(p => p.Value);
                        }

                        resp.StatusCode = (int) HttpStatusCode.OK;
                        return resp.WriteAsJson(new UsersListResponse
                        {
                            Users = users
                        });
                    })
                    .MapGet("api/v2/users/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        if (!state.Users.ContainsKey(id))
                        {
                            resp.StatusCode = (int) HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var user = state.Users[id];

                        resp.StatusCode = (int) HttpStatusCode.OK;
                        return resp.WriteAsJson(new SingleUserResponse
                        {
                            UserResponse = user
                        });
                    })
                    .MapGet("api/v2/users", (req, resp, routeData) =>
                    {
                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        resp.StatusCode = (int) HttpStatusCode.OK;
                        return resp.WriteAsJson(new UsersListResponse
                        {
                            Users = state.Users.Values
                        });
                    })
                    .MapGet("api/v2/groups/{id}/users", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var users = state
                            .Users
                            .Where(x => x.Value.DefaultGroupId == id)
                            .Select(p => p.Value);

                        resp.StatusCode = (int) HttpStatusCode.OK;
                        return resp.WriteAsJson(new UsersListResponse
                        {
                            Users = users
                        });
                    })
                    .MapGet("api/v2/organizations/{id}/users", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var users = state
                            .Users
                            .Where(x => x.Value.OrganizationId.HasValue)
                            .Where(x => x.Value.OrganizationId == id)
                            .Select(p => p.Value);

                        resp.StatusCode = (int) HttpStatusCode.OK;
                        return resp.WriteAsJson(new UsersListResponse
                        {
                            Users = users
                        });
                    })
                    .MapPost("api/v2/users", (req, resp, routeData) =>
                    {
                        var user = req.Body.ReadAs<UserRequest<UserCreateRequest>>().User;

                        if (user.Tags != null && user.Tags.Contains("error"))
                        {
                            resp.StatusCode = (int) HttpStatusCode.PaymentRequired; // It doesnt matter as long as not 201

                            return Task.CompletedTask;
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();
                        var mapper = req.HttpContext.RequestServices.GetRequiredService<IMapper>();

                        var userNew = mapper.Map<UserResponse>(user);

                        userNew.Id = long.Parse(Rand.Next().ToString());
                        userNew.Url = new Uri($"https://company.zendesk.com/api/v2/users/{userNew.Id}.json");
                        state.Users.Add(userNew.Id, userNew);

                        resp.StatusCode = (int) HttpStatusCode.Created;
                        return resp.WriteAsJson(new SingleUserResponse
                        {
                            UserResponse = userNew
                        });
                    })
                    .MapPut("api/v2/users/{id}", (req, resp, routeData) =>
                    {
                        var user = req.Body.ReadAs<UserRequest<UserUpdateRequest>>().User;

                        var id = long.Parse(routeData.Values["id"].ToString());

                        if (user.Tags != null && user.Tags.Contains("error"))
                        {
                            resp.StatusCode = (int) HttpStatusCode.BadRequest;

                            return Task.CompletedTask;
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();
                        
                        var mapper = req.HttpContext.RequestServices.GetRequiredService<IMapper>();
                        mapper.Map(user, state.Users[id]);

                        resp.StatusCode = (int) HttpStatusCode.Created;
                        return resp.WriteAsJson(new
                        {
                            user
                        });
                    })
                    .MapDelete("api/v2/users/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        state.Users.Remove(id);

                        resp.StatusCode = (int) HttpStatusCode.NoContent;
                        return Task.CompletedTask;
                    });
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
