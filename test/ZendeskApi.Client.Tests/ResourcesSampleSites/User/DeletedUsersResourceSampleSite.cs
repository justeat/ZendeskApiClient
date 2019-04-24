using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class DeletedUsersResourceSampleSite : SampleSite<UserResponse>
    {
        public DeletedUsersResourceSampleSite(string resource)
            : base(
                resource, 
                MatchesRequest, 
                ConfigureWebHost,
                state =>
                {
                    state.Items.Add(1, new UserResponse
                    {
                        Id = 1,
                        Name = "Kung Fu Wizard",
                        Email = "Fu1@fu.com"
                    });

                    state.Items.Add(2, new UserResponse()
                    {
                        Id = 2,
                        Name = "some name",
                        Email = "Fu2@fu.com"
                    });
                })
        { }

        private static void ConfigureWebHost(WebHostBuilder builder)
        {
            builder
                .ConfigureServices(services => {
                    services.AddSingleton(_ => new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<UserUpdateRequest, UserResponse>();
                        cfg.CreateMap<UserCreateRequest, UserResponse>();
                    }).CreateMapper());
                });
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/deleted_users/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<UserResponse>>();

                        if (!state.Items.ContainsKey(id))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var user = state.Items[id];

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new SingleUserResponse
                        {
                            UserResponse = user
                        });
                    })
                    .MapGet("api/v2/deleted_users", (req, resp, routeData) =>
                    {
                        var state = req.HttpContext.RequestServices.GetRequiredService<State<UserResponse>>();

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new UsersListResponse
                        {
                            Users = state.Items.Values
                        });
                    })
                    .MapDelete("api/v2/deleted_users/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<UserResponse>>();

                        state.Items.Remove(id);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return Task.CompletedTask;
                    });
            }
        }
    }
}
