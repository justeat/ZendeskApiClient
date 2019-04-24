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
                PopulateState)
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

        private static void PopulateState(State<UserResponse> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new UserResponse
                {
                    Id = i,
                    Name = $"name.{i}",
                    Email = $"email.{i}",
                    ExternalId = i.ToString()
                });
            }
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/deleted_users/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.GetById<SingleUserResponse, UserResponse>(
                            req,
                            resp,
                            routeData,
                            item => new SingleUserResponse
                            {
                                UserResponse = item
                            });
                    })
                    .MapGet("api/v2/deleted_users", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<UsersListResponse, UserResponse>(
                            req,
                            resp,
                            items => new UsersListResponse
                            {
                                Users = items,
                                Count = items.Count
                            });
                    })
                    .MapDelete("api/v2/deleted_users/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.Delete<UserResponse>(
                            req,
                            resp,
                            routeData,
                            HttpStatusCode.OK);
                    });
            }
        }
    }
}
