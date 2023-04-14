using System;
using Microsoft.AspNetCore.Routing;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class GroupsResourceSampleSite : SampleSite<Group>
    {
        public GroupsResourceSampleSite(string resource)
            : base(
                resource, 
                MatchesRequest,
                null,
                PopulateState)
        { }

        private static void PopulateState(State<Group> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new Group
                {
                    Id = i,
                    Name = $"name.{i}",
                    Url = new Uri($"https://company.zendesk.com/api/v2/groups/{i}.json")
                });
            }
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/groups/assignable", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<GroupListResponse, Group>(
                            req,
                            resp,
                            items => new GroupListResponse
                            {
                                Groups = items,
                                Count = items.Count
                            });
                    })
                    .MapGet("api/v2/groups/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.GetById<GroupResponse, Group>(
                            req,
                            resp,
                            routeData,
                            item => new GroupResponse
                            {
                                Group = item
                            });
                    })
                    .MapGet("api/v2/groups", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<GroupListResponse, Group>(
                            req,
                            resp,
                            items => new GroupListResponse
                            {
                                Groups = items,
                                Count = items.Count
                            });
                    })
                    .MapGet("api/v2/users/{id}/groups", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<GroupListResponse, Group>(
                            req,
                            resp,
                            items => new GroupListResponse
                            {
                                Groups = items,
                                Count = items.Count
                            });
                    })
                    .MapPost("api/v2/groups", async (req, resp, routeData) =>
                    {
                        var create = await req.ReadAsync<GroupRequest<GroupCreateRequest>>();

                        var id = string.IsNullOrWhiteSpace(create.Group.Name) ? int.MinValue : long.Parse(Rand.Next().ToString());

                        await RequestHelper.Create(
                            req,
                            resp,
                            routeData,
                            item => item.Id,
                            new Group {
                                Id = id,
                                Name = create.Group.Name,
                                Url = new Uri($"https://company.zendesk.com/api/v2/groups/{id}.json")
                            }, 
                            item => new GroupResponse
                            {
                                Group = item
                            });
                    })
                    .MapPut("api/v2/groups/{id}", async (req, resp, routeData) =>
                    {
                        var update = await req.ReadAsync<GroupRequest<GroupUpdateRequest>>();

                        await RequestHelper.Update(
                            req,
                            resp,
                            routeData,
                            new Group
                            {
                                Id = update.Group.Id,
                                Name = update.Group.Name,
                                Url = new Uri($"https://company.zendesk.com/api/v2/groups/{update.Group.Id}.json")
                            },
                            item => new GroupResponse
                            {
                                Group = item
                            });
                    })
                    .MapDelete("api/v2/groups/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.Delete<Group>(
                            req,
                            resp,
                            routeData);
                    });
            }
        }
    }
}
