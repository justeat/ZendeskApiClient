using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.Extensions;

namespace ZendeskApi.Client.Tests.ResourcesSampleSites
{
    class OrganizationResourceSampleSite : SampleSite
    {
        private class State
        {
            public readonly IDictionary<long, Organization> Organizations = new Dictionary<long, Organization>();

            public State()
            {
                for (var i = 1; i <= 100; i++)
                {
                    Organizations.Add(i, new Organization
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
        }

        public static Action<IRouteBuilder> MatchesRequest
        {
            get
            {
                return rb => rb
                    .MapGet("api/v2/organizations", (req, resp, routeData) =>
                    {
                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        var organizations = state.Organizations
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
                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

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

                        var organizations = state.Organizations
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
                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

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
                            organizations = state.Organizations
                                .Select(x => x.Value)
                                .Where(x => ids.Contains(x.Id))
                                .ToList();
                        }
                        else if (req.Query.ContainsKey("external_ids"))
                        {
                            organizations = state.Organizations
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

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        if (id == int.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        if (!state.Organizations.ContainsKey(id))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        var org = state.Organizations.Single(x => x.Key == id).Value;

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

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        org.Id = long.Parse(Rand.Next().ToString());

                        state.Organizations.Add(org.Id, org);

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

                        var state = req.HttpContext.RequestServices.GetRequiredService<State>();

                        if (id == int.MinValue)
                        {
                            resp.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                            return Task.FromResult(resp);
                        }

                        if (!state.Organizations.ContainsKey(id))
                        {
                            resp.StatusCode = (int)HttpStatusCode.NotFound;
                            return Task.CompletedTask;
                        }

                        state.Organizations[id] = org;

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

        private readonly TestServer _server;

        private HttpClient _client;
        public override HttpClient Client => _client;

        public OrganizationResourceSampleSite(string resource)
        {
            var webhostbuilder = new WebHostBuilder();
            webhostbuilder
                .ConfigureServices(services =>
                {
                    services.AddSingleton(_ => new State());
                    services.AddRouting();
                    services.AddMemoryCache();
                })
                .Configure(app =>
                {
                    app.UseRouter(MatchesRequest);
                });

            _server = new TestServer(webhostbuilder);

            RefreshClient(resource);
        }

        public override void RefreshClient(string resource)
        {
            _client = _server.CreateClient();
            _client.BaseAddress = new Uri($"http://localhost/{CreateResource(resource)}");
        }

        private static string CreateResource(string resource)
        {
            resource = resource?.Trim('/');

            return resource != null ? resource + "/" : null;
        }

        public Uri BaseUri => Client.BaseAddress;

        public override void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}
