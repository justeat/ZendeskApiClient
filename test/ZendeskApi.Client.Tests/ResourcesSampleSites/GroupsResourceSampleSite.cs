using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class GroupsResourceSampleSite : SampleSite<Group>
    {
        public GroupsResourceSampleSite(string resource)
            : base(resource, MatchesRequest)
        { }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/groups/assignable", (req, resp, routeData) =>
                    {
                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Group>>();

                        var groups = state
                            .Items
                            .Where(x => x.Value.Name.Contains("Assign:true"))
                            .Select(p => p.Value);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new GroupListResponse { Groups = groups });
                    })
                    .MapGet("api/v2/groups/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Group>>();

                        if (!state.Items.ContainsKey(id))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var group = state.Items.Single(x => x.Key == id).Value;

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new GroupResponse{ Group = group});
                    })
                    .MapGet("api/v2/groups", (req, resp, routeData) =>
                    {
                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Group>>();

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new GroupListResponse { Groups = state.Items.Values });
                    })
                    .MapGet("api/v2/users/{id}/groups", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Group>>();

                        var groups = state
                            .Items
                            .Where(x => x.Value.Name.Contains($"USER: {id}"))
                            .Select(p => p.Value);

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new GroupListResponse { Groups = groups });
                    })
                    .MapPost("api/v2/groups", (req, resp, routeData) =>
                    {
                        var groupRequest = req.Body.ReadAs<GroupRequest<GroupCreateRequest>>();
                        var group = groupRequest.Group;
                        
                        if (group.Name.Contains("error"))
                        {
                            resp.StatusCode = (int)HttpStatusCode.PaymentRequired; // It doesnt matter as long as not 201

                            return Task.CompletedTask;
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Group>>();
                        var groupId = long.Parse(Rand.Next().ToString());
                        var newGroup = new Group
                        {
                            Name = group.Name,
                            Id = groupId,
                            Url = new Uri("https://company.zendesk.com/api/v2/groups/" + groupId + ".json")
                        };
                        
                        state.Items.Add(newGroup.Id, newGroup);

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        
                        return resp.WriteAsJson(new GroupResponse{Group = newGroup});
                    })
                    .MapPut("api/v2/groups/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var groupRequest = req.Body.ReadAs<GroupRequest<GroupUpdateRequest>>();
                        var group = groupRequest.Group;

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Group>>();
                        var newGroup = new Group
                        {
                            Name = group.Name,
                            Id = id
                        };

                        state.Items[id] = newGroup;

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new GroupResponse{Group = state.Items[id]});
                    })
                    .MapDelete("api/v2/groups/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Group>>();

                        state.Items.Remove(id);

                        resp.StatusCode = (int)HttpStatusCode.NoContent;
                        return Task.CompletedTask;
                    });
            }
        }
    }
}
