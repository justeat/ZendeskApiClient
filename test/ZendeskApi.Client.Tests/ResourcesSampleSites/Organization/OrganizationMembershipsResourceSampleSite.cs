using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    internal class OrganizationMembershipsResourceSampleSite : SampleSite<OrganizationMembership>
    {
        public OrganizationMembershipsResourceSampleSite(string resource) 
            : base(
                resource, 
                MatchesRequest, 
                null, 
                PopulateState)
        {
        }

        private static void PopulateState(State<OrganizationMembership> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new OrganizationMembership
                {
                    Id = i,
                    UserId = i,
                    OrganizationId = i
                });
            }
        }

        private static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/organization_memberships", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<OrganizationMembershipsResponse, OrganizationMembership>(
                            req,
                            resp,
                            items => new OrganizationMembershipsResponse
                            {
                                OrganizationMemberships = items,
                                Count = items.Count
                            });
                    })
                    .MapGet("api/v2/users/{userId}/organization_memberships", (req, resp, routeData) =>
                    {
                        return RequestHelper.FilteredList<OrganizationMembershipsResponse, OrganizationMembership>(
                            req,
                            resp,
                            routeData.Values["userId"].ToString(),
                            (id, items) => items.Where(x => x.UserId == id),
                            items => new OrganizationMembershipsResponse
                            {
                                OrganizationMemberships = items,
                                Count = items.Count
                            });
                    })
                    .MapGet("api/v2/organizations/{organizationId}/organization_memberships", (req, resp, routeData) =>
                    {
                        return RequestHelper.FilteredList<OrganizationMembershipsResponse, OrganizationMembership>(
                            req,
                            resp,
                            routeData.Values["organizationId"].ToString(),
                            (id, items) => items.Where(x => x.OrganizationId == id),
                            items => new OrganizationMembershipsResponse
                            {
                                OrganizationMemberships = items,
                                Count = items.Count
                            });
                    })
                    .MapGet("api/v2/organization_memberships/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.GetById<OrganizationMembershipResponse, OrganizationMembership>(
                            req,
                            resp,
                            routeData,
                            item => new OrganizationMembershipResponse
                            {
                                OrganizationMembership = item
                            });
                    })
                    .MapGet("api/v2/users/{userId}/organization_memberships/{organizationId}", (req, resp, routeData) =>
                    {
                        var userId = long.Parse(routeData.Values["userId"].ToString());
                        var organizationId = long.Parse(routeData.Values["organizationId"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<OrganizationMembership>>();

                        if (userId == int.MinValue && organizationId == int.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        if (state.Items.All(x => x.Value.UserId != userId && x.Value.OrganizationId != organizationId))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var item = state.Items.Single(x => x.Value.UserId == userId && x.Value.OrganizationId == organizationId)
                            .Value;

                        resp.StatusCode = (int)HttpStatusCode.OK;

                        return resp.WriteAsJson(new OrganizationMembershipResponse
                        {
                            OrganizationMembership = item
                        });
                    })
                    .MapPost("api/v2/organization_memberships", async (req, resp, routeData) =>
                    {
                        var request = await req.ReadAsync<OrganizationMembershipCreateRequest>();
                        var membership = request.OrganizationMembership;

                        await RequestHelper.Create(
                            req,
                            resp,
                            routeData,
                            item => item.Id,
                            membership,
                            item => new OrganizationMembershipResponse
                            {
                                OrganizationMembership = item
                            });
                    })
                    .MapPost("api/v2/users/{userId}/organization_memberships", async (req, resp, routeData) =>
                    {
                        var request = await req.ReadAsync<OrganizationMembershipCreateRequest>();
                        var membership = request.OrganizationMembership;

                        await RequestHelper.Create(
                            req,
                            resp,
                            routeData,
                            item => item.Id,
                            membership,
                            item => new OrganizationMembershipResponse
                            {
                                OrganizationMembership = item
                            });
                    })
                    .MapPost("api/v2/organization_memberships/create_many", async (req, resp, routeData) =>
                    {
                        var request = await req.ReadAsync<OrganizationMembershipsRequest>();
                        var memberships = request.Item;

                        await RequestHelper.CreateMany(
                            req,
                            resp,
                            routeData,
                            item => item.Id,
                            memberships,
                            items => new JobStatusResponse
                            {
                                Total = items.Count()
                            });
                    })
                    .MapPut("api/v2/users/{userId}/organization_memberships/{membership_id}/make_default.json", (req, resp, routeData) =>
                    {
                        var userId = long.Parse(routeData.Values["userId"].ToString());
                        var membershipId = long.Parse(routeData.Values["membership_id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<OrganizationMembership>>();

                        if (userId == int.MinValue && membershipId == int.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        if (state.Items.All(x => x.Value.UserId != userId && x.Value.OrganizationId != membershipId))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var item = state.Items.Single(x => x.Value.UserId == userId && x.Value.OrganizationId == membershipId)
                            .Value;

                        item.Default = true;

                        resp.StatusCode = (int)HttpStatusCode.OK;

                        return resp.WriteAsJson(new OrganizationMembershipsResponse
                        {
                            OrganizationMemberships = new List<OrganizationMembership> { item },
                            Count = 1
                        });
                    })
                    .MapDelete("api/v2/organization_memberships/destroy_many.json", (req, resp, routeData) =>
                    {
                        return RequestHelper.DeleteMany<OrganizationMembership>(
                            req,
                            resp);
                    })
                    .MapDelete("api/v2/organization_memberships/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.Delete<OrganizationMembership>(
                            req,
                            resp,
                            routeData);
                    })
                    .MapDelete("api/v2/users/{userId}/organization_memberships/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.Delete<OrganizationMembership>(
                            req,
                            resp,
                            routeData);
                    });
            }
        }
    }
}
