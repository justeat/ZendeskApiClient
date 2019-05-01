using System;
using System.Collections.Generic;
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

    internal class OrganizationResourceSampleSite : SampleSite<Organization>
    {
        public OrganizationResourceSampleSite(string resource)
            : base(
                resource, 
                MatchesRequest, 
                null, 
                PopulateState)
        { }

        private static void PopulateState(State<Organization> state)
        {
            for (var i = 1; i <= 100; i++)
            {
                state.Items.Add(i, new Organization
                {
                    Id = i,
                    Name = $"org.{i}",
                    ExternalId = i.ToString(),
                    CustomFields = new Dictionary<object, object>
                    {
                        { "requester", i.ToString() }
                    }
                });
            }
        }

        private static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/organizations", (req, resp, routeData) =>
                    {
                        return RequestHelper.List<OrganizationsResponse, Organization>(
                            req,
                            resp,
                            orgs => new OrganizationsResponse
                            {
                                Organizations = orgs,
                                Count = orgs.Count
                            });
                    })
                    .MapGet("api/v2/users/{userId}/organizations", (req, resp, routeData) =>
                    {
                        return RequestHelper.FilteredList<OrganizationsResponse, Organization>(
                            req,
                            resp,
                            routeData.Values["userId"].ToString(),
                            (id, items) => items.Where(x => x.CustomFields.ContainsKey("requester") && x.CustomFields["requester"].ToString() == id.ToString()),
                            items => new OrganizationsResponse
                            {
                                Organizations = items,
                                Count = items.Count
                            });
                    })
                    .MapGet("api/v2/organizations/show_many", (req, resp, routeData) =>
                    {
                        return RequestHelper.Many<OrganizationsResponse, Organization>(
                            req,
                            resp,
                            org => org.Id,
                            org => org.ExternalId,
                            items => new OrganizationsResponse
                            {
                                Organizations = items,
                                Count = items.Count
                            });
                    })
                    .MapGet("api/v2/organizations/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.GetById<OrganizationResponse, Organization>(
                            req,
                            resp,
                            routeData,
                            item => new OrganizationResponse
                            {
                                Organization = item
                            });
                    })
                    .MapPost("api/v2/organizations", (req, resp, routeData) =>
                    {
                        var request = req.Body.ReadAs<OrganizationCreateRequest>();
                        var org = new Organization
                        {
                            Name = request.Organization.Name,
                            Details = request.Organization.Details,
                            Notes = request.Organization.Notes,
                            CustomFields = request.Organization.CustomFields,
                            Tags = request.Organization.Tags,
                            ExternalId = request.Organization.ExternalId,
                            DomainNames = request.Organization.DomainNames,
                            SharedTickets = request.Organization.SharedTickets,
                            SharedComments = request.Organization.SharedComments,
                            GroupId = request.Organization.GroupId
                        };

                        if (string.IsNullOrEmpty(org.Name))
                        {
                            resp.StatusCode = (int)HttpStatusCode.PaymentRequired; // It doesnt matter as long as not 201

                            return Task.CompletedTask;
                        }

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Organization>>();

                        org.Id = long.Parse(Rand.Next().ToString());

                        state.Items.Add(org.Id, org);

                        resp.StatusCode = (int)HttpStatusCode.Created;
                        return resp.WriteAsJson(new OrganizationResponse
                        {
                            Organization = org
                        });
                    })
                    .MapPut("api/v2/organizations/{id}", (req, resp, routeData) =>
                    {
                        var request = req.Body
                            .ReadAs<OrganizationUpdateRequest>();

                        return RequestHelper.Update<OrganizationResponse, Organization>(
                            req,
                            resp,
                            routeData,
                            new Organization
                            {
                                Id = request.Organization.Id,
                                Name = request.Organization.Name,
                                Details = request.Organization.Details,
                                Notes = request.Organization.Notes,
                                CustomFields = request.Organization.CustomFields,
                                Tags = request.Organization.Tags,
                                ExternalId = request.Organization.ExternalId,
                                DomainNames = request.Organization.DomainNames,
                                SharedTickets = request.Organization.SharedTickets,
                                SharedComments = request.Organization.SharedComments,
                                GroupId = request.Organization.GroupId
                            },
                            item => new OrganizationResponse
                            {
                                Organization = item
                            });
                    })
                    .MapDelete("api/v2/organizations/{id}", (req, resp, routeData) =>
                    {
                        return RequestHelper.Delete<Organization>(
                            req,
                            resp,
                            routeData);
                    });
            }
        }
    }
}
