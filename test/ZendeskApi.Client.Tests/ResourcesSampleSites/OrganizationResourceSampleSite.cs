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
                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Organization>>();

                        var organizations = state.Items
                            .Select(x => x.Value)
                            .ToList();

                        if (req.Query.ContainsKey("page") &&
                            req.Query.ContainsKey("per_page") &&
                            int.TryParse(req.Query["page"].ToString(), out var page) &&
                            int.TryParse(req.Query["per_page"].ToString(), out var size))
                        {
                            if (page == int.MaxValue && size == int.MaxValue)
                            {
                                resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                                return Task.FromResult(resp);
                            }
                            
                            organizations = organizations
                                .Skip((page - 1) * size)
                                .Take(size)
                                .ToList();
                        }

                        resp.StatusCode = (int)HttpStatusCode.OK;

                        return resp.WriteAsJson(new OrganizationsResponse
                        {
                            Organizations = organizations,
                            Count = organizations.Count
                        });
                    })
                    .MapGet("api/v2/users/{userId}/organizations", (req, resp, routeData) =>
                    {
                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Organization>>();

                        var id = long.Parse(routeData.Values["userId"].ToString());

                        if (id == int.MaxValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.FromResult(resp);
                        }

                        if (id == int.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        var organizations = state.Items
                            .Select(x => x.Value)
                            .Where(x => x.CustomFields.ContainsKey("requester") && x.CustomFields["requester"].ToString() == id.ToString())
                            .ToList();

                        if (req.Query.ContainsKey("page") &&
                            req.Query.ContainsKey("per_page") &&
                            int.TryParse(req.Query["page"].ToString(), out var page) &&
                            int.TryParse(req.Query["per_page"].ToString(), out var size))
                        {
                            organizations = organizations
                                .Skip((page - 1) * size)
                                .Take(size)
                                .ToList();
                        }

                        resp.StatusCode = (int)HttpStatusCode.OK;

                        return resp.WriteAsJson(new OrganizationsResponse
                        {
                            Organizations = organizations,
                            Count = organizations.Count
                        });
                    })
                    .MapGet("api/v2/organizations/show_many", (req, resp, routeData) =>
                    {
                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Organization>>();

                        IList<Organization> organizations = new List<Organization>();

                        var idsQuery = req.Query.ContainsKey("ids")
                            ? req.Query["ids"].ToString()
                            : req.Query["external_ids"].ToString();

                        var ids = idsQuery
                            .Split(',')
                            .Select(long.Parse)
                            .ToList();

                        if (ids.First() == long.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        if (req.Query.ContainsKey("ids"))
                        {
                            organizations = state.Items
                                .Select(x => x.Value)
                                .Where(x => ids.Contains(x.Id))
                                .ToList();
                        }
                        else if (req.Query.ContainsKey("external_ids"))
                        {
                            organizations = state.Items
                                .Select(x => x.Value)
                                .Where(x => ids.Contains(long.Parse(x.ExternalId)))
                                .ToList();
                        }

                        if (req.Query.ContainsKey("page") &&
                            req.Query.ContainsKey("per_page") &&
                            int.TryParse(req.Query["page"].ToString(), out var page) &&
                            int.TryParse(req.Query["per_page"].ToString(), out var size))
                        {
                            organizations = organizations
                                .Skip((page - 1) * size)
                                .Take(size)
                                .ToList();
                        }

                        resp.StatusCode = (int)HttpStatusCode.OK;

                        return resp.WriteAsJson(new OrganizationsResponse
                        {
                            Organizations = organizations,
                            Count = organizations.Count
                        });
                    })
                    .MapGet("api/v2/organizations/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Organization>>();

                        if (id == int.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        if (!state.Items.ContainsKey(id))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var org = state.Items.Single(x => x.Key == id).Value;

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new OrganizationResponse
                        {
                            Organization = org
                        });
                    })
                    .MapPost("api/v2/organizations", (req, resp, routeData) =>
                    {
                        var request = req.Body.ReadAs<OrganizationCreateRequest>();
                        var org = request.Organization;

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
                        var request = req.Body.ReadAs<OrganizationUpdateRequest>();
                        var org = request.Organization;

                        var id = long.Parse(routeData.Values["id"].ToString());

                        var state = req.HttpContext.RequestServices.GetRequiredService<State<Organization>>();

                        if (id == int.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        if (!state.Items.ContainsKey(id))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        state.Items[id] = org;

                        resp.StatusCode = (int)HttpStatusCode.OK;
                        return resp.WriteAsJson(new OrganizationResponse
                        {
                            Organization = org
                        });
                    })
                    .MapDelete("api/v2/organizations/{id}", (req, resp, routeData) =>
                    {
                        var id = long.Parse(routeData.Values["id"].ToString());

                        if (id == int.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        resp.StatusCode = (int)HttpStatusCode.NoContent;
                        return Task.FromResult(resp);
                    });
            }
        }
    }
}
