using System;
using Microsoft.AspNetCore.Routing;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

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
                    .MapPost("api/v2/groups", (req, resp, routeData) =>
                    {
                        var create = req.Body
                            .ReadAs<GroupRequest<GroupCreateRequest>>()
                            .Group;

                        var id = string.IsNullOrWhiteSpace(create.Name) ? int.MinValue : long.Parse(Rand.Next().ToString());

                        return RequestHelper.Create<GroupResponse, Group>(
                            req,
                            resp,
                            routeData,
                            item => item.Id,
                            new Group {
                                Id = id,
                                Name = create.Name,
                                Url = new Uri($"https://company.zendesk.com/api/v2/groups/{id}.json")
                            }, 
                            item => new GroupResponse
                            {
                                Group = item
                            });
                    })
                    .MapPut("api/v2/groups/{id}", (req, resp, routeData) =>
                    {
                        var update = req.Body
                            .ReadAs<GroupRequest<GroupUpdateRequest>>()
                            .Group;

                        return RequestHelper.Update<GroupResponse, Group>(
                            req,
                            resp,
                            routeData,
                            new Group
                            {
                                Id = update.Id,
                                Name = update.Name,
                                Url = new Uri($"https://company.zendesk.com/api/v2/groups/{update.Id}.json")
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
